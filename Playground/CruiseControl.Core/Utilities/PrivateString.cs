﻿namespace CruiseControl.Core.Utilities
{
    using System.ComponentModel;
    using CruiseControl.Core.Interfaces;

    /// <summary>
    /// Stores a string that can be access either publicly (and is hidden) or privately (accessed normally).
    /// </summary>
    [TypeConverter(typeof(PrivateStringTypeConverter))]
    public sealed class PrivateString
        : ISecureData
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateString"/> class.
        /// </summary>
        public PrivateString()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateString"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public PrivateString(string value)
        {
            this.PrivateValue = value;
        }
        #endregion

        #region Public properties
        #region PrivateValue
        /// <summary>
        /// Gets or sets the private value.
        /// </summary>
        /// <value>The private (actual) value.</value>
        public string PrivateValue { get; set; }
        #endregion

        #region PublicValue
        /// <summary>
        /// Gets the public value.
        /// </summary>
        /// <value>The public value.</value>
        public string PublicValue
        {
            get { return "********"; }
        }
        #endregion
        #endregion

        #region Public methods
        #region ToString()
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        /// This will return the data in public (hidden) mode.
        /// </remarks>
        public override string ToString()
        {
            return this.ToString(SecureDataMode.Public);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance in the specified data mode.
        /// </summary>
        /// <param name="dataMode">The data mode to use.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(SecureDataMode dataMode)
        {
            switch (dataMode)
            {
                case SecureDataMode.Private:
                    return this.PrivateValue;

                default:
                    return this.PublicValue;
            }
        }
        #endregion
        #endregion

        #region Operators
        #region implicit
        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="PrivateString"/>.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PrivateString(string args)
        {
            return new PrivateString { PrivateValue = args };
        }
        #endregion
        #endregion
    }
}
