using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace ArendaAvto.Converter
{
    internal class ImageConverter : IValueConverter
    {
        private static Bitmap _defaultImage;
        private static readonly Dictionary<string, Bitmap> _cache = new();

        // Статический конструктор для инициализации статических полей
        static ImageConverter()
        {
            _defaultImage = LoadDefaultImage();
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string imageUrl)
                return _defaultImage;

            // Если картинка уже загружена, вернуть её сразу
            if (_cache.TryGetValue(imageUrl, out var cachedImage))
                return cachedImage;

            // Сначала вернуть заглушку
            LoadImageAsync(imageUrl);
            return _defaultImage;
        }

        private async void LoadImageAsync(string imageUrl)
        {
            try
            {
                using var client = new HttpClient();
                var imageBytes = await client.GetByteArrayAsync(imageUrl);
                using var ms = new MemoryStream(imageBytes);
                var bitmap = new Bitmap(ms);

                // Кэшируем загруженное изображение
                _cache[imageUrl] = bitmap;

                // Обновить UI, вызвав пересчет привязки
                RxApp.MainThreadScheduler.Schedule(() =>
                {
                    // Обновление кэша
                    _cache[imageUrl] = bitmap;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
                // В случае ошибки, используем изображение по умолчанию
                _cache[imageUrl] = _defaultImage;
            }
        }

        private static Bitmap LoadDefaultImage()
        {
            try
            {
                var path = Path.GetFullPath(Environment.CurrentDirectory + @"\Images\default.png");
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Default image not found.", path);
                }
                return new Bitmap(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки изображения по умолчанию: {ex.Message}");
                // Вернуть null или другое значение по умолчанию
                return null; // Или можно вернуть пустой Bitmap, если это необходимо
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}