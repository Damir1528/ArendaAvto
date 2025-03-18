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
        private static readonly Bitmap DefaultImage = LoadDefaultImage();
        private static readonly Dictionary<string, Bitmap> _cache = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string imageUrl)
                return DefaultImage;

            // Если картинка уже загружена, вернуть её сразу
            if (_cache.TryGetValue(imageUrl, out var cachedImage))
                return cachedImage;

            // Сначала вернуть заглушку
            LoadImageAsync(imageUrl);
            return DefaultImage;
        }

        private async void LoadImageAsync(string imageUrl)
        {
            try
            {
                using var client = new HttpClient();
                var imageBytes = await client.GetByteArrayAsync(imageUrl);
                using var ms = new MemoryStream(imageBytes);
                var bitmap = new Bitmap(ms);

                _cache[imageUrl] = bitmap;

                // Обновить UI, вызвав пересчёт привязки (требует ReactiveUI или INotifyPropertyChanged)
                RxApp.MainThreadScheduler.Schedule(() =>
                {
                    _cache[imageUrl] = bitmap;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
                _cache[imageUrl] = DefaultImage;
            }
        }

        private static Bitmap LoadDefaultImage()
        {
            return new Bitmap(Path.GetFullPath(Environment.CurrentDirectory + @"\Images\default.png"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}