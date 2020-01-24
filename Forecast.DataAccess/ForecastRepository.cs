using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Forecast.Core.Models;
using Forecast.DataAccess.Interfaces;
using MySql.Data.MySqlClient;

namespace Forecast.DataAccess
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly string connectionString;

        public ForecastRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ForecastDb"].ConnectionString;
        }

        public async Task InsertWeatherData(IEnumerable<City> payload)
        {
            var cityTasks = payload
                .Where(city => city.IsValid())
                .Select(async city =>
                {
                    var cityId = await InsertCity(city);

                    var weatherTasks = city.Weathers
                        .Where(weather => weather.IsValid())
                        .Select(async weather =>
                        {
                            object typeId = null;

                            if (weather.Type != null)
                                typeId = await InsertWeatherType(weather.Type);

                            await InsertWeather(weather, cityId, typeId);
                        });

                    await Task.WhenAll(weatherTasks);
                });

            await Task.WhenAll(cityTasks);
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            var query = @"  SELECT id, name 
                            FROM forecast.cities;";
            var cities = await WithConnection(async c => await c.QueryAsync<City>(query));
            return cities;
        }

        public async Task<IEnumerable<Weather>> GetWeatherData(int cityId, DateTime date)
        {
            var query = @"  SELECT *
                            FROM forecast.weathers w
                            LEFT JOIN forecast.weathertypes t
                            ON t.id = w.type_id
                            WHERE   city_id = @cityId AND
                                    DATE(date) = DATE(@date);";
            var weathers = await WithConnection(async c => await c.QueryAsync<Weather, WeatherType, Weather>(query,
                (weather, type) =>
                {
                    weather.Type = type;
                    return weather;
                }, new
                {
                    cityId,
                    date
                }));
            return weathers;
        }

        private async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> query)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                return await query(connection);
            }
        }


        private async Task<object> InsertCity(City city)
        {
            var query = @"  INSERT IGNORE INTO forecast.cities (name) 
                                VALUES (@name);
                            SELECT id FROM forecast.cities
                                WHERE name = @name;";
            var cityId = await WithConnection(async c => await c.ExecuteScalarAsync(query, city));
            return cityId;
        }

        private async Task<object> InsertWeatherType(WeatherType weatherType)
        {
            var query = @"  INSERT IGNORE INTO forecast.weathertypes (name) 
                                VALUES (@name);
                            SELECT id FROM forecast.weathertypes
                                WHERE name = @name;";
            var typeId = await WithConnection(async c => await c.ExecuteScalarAsync(query, weatherType));
            return typeId;
        }

        private async Task InsertWeather(Weather weather, object cityId, object typeId)
        {
            var query = @"  INSERT INTO forecast.weathers (date, temperature, city_id, type_id) 
                                    VALUES (@date, @temperature, @cityId, @typeId);";
            await WithConnection(async c => await c.ExecuteAsync(query, new
            {
                date = weather.Date,
                temperature = weather.Temperature,
                cityId = cityId,
                typeId = typeId
            }));
        }
    }
}
