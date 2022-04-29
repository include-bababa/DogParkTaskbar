// -----------------------------------------------------------------------
// <copyright file="ValueScaler.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Data
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Value converter which scales the value by the parameter.
    /// </summary>
    internal class ValueScaler : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseValue = System.Convert.ToDouble(value);
            var scaleValue = System.Convert.ToDouble(parameter);
            return baseValue * scaleValue;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseValue = System.Convert.ToDouble(value);
            var scaleValue = System.Convert.ToDouble(parameter);
            return baseValue / scaleValue;
        }
    }
}
