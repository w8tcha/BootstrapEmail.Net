namespace BootstrapEmail.Net;

/// <summary>
/// For globally or multiple times used constants
/// </summary>
public static class Constants
{
    /// <summary>
    /// Template Content constants
    /// </summary>
    public struct Template
    {
        /// <summary>
        /// The classes
        /// </summary>
        public const string Classes = "{{ classes }}";

        /// <summary>
        /// The contents
        /// </summary>
        public const string Contents = "{{ contents }}";
    }

    /// <summary>
    /// Struct SassTypes
    /// </summary>
    public struct SassTypes
    {
	    /// <summary>
	    /// The sass main email file name
	    /// </summary>
	    public const string SassEmail = "bootstrap-email";

		/// <summary>
		/// the sass header file name.
		/// </summary>
		public const string Head = "bootstrap-head";
    }

	/// <summary>
	/// The core templates folder name
	/// </summary>
	public const string CoreTemplatesDir = "templates";

    /// <summary>
    /// The sass cache folder name
    /// </summary>
    public const string SassCache = ".sass-cache";

    /// <summary>
    /// The default output folder
    /// </summary>
    public const string Compiled = "compiled";
}