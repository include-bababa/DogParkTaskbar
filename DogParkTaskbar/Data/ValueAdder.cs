// -----------------------------------------------------------------------
// <copyright file="ValueAdder.cs" company="Bababa">
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
    /// Value converter which adds the parameter to the value.
    /// </summary>
    internal class ValueAdder : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseValue = System.Convert.ToDouble(value);
            var addValue = System.Convert.ToDouble(parameter);
            return baseValue + addValue;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseValue = System.Convert.ToDouble(value);
            var addValue = System.Convert.ToDouble(parameter);
            return baseValue - addValue;
        }
    }
}
