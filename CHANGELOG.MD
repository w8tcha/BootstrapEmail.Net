## 2.0.0

-  Sass is now compiled in managed code with DartSassHost and Jint, so no platform specific Dart Sass runtime is needed
-  the default stylesheets are included precompiled, only custom Sass gets compiled
-  **Breaking:** removed `ConfigStore.DartSassNativeType`, and the `DartSass.Native.*` packages are no longer used
-  **Breaking:** `StyleType` is now `BootstrapEmail.Net.StyleType` instead of the Citizen17.DartSass type
-  **Breaking:** renamed the `BootstrapEmail` class to `BootstrapEmailCompiler`, because `new BootstrapEmail()` didn't compile next to the namespace of the same name
-  **Breaking:** the CLI option `--text` is now a flag that takes no value
-  fixed pattern input compiling the file path instead of the file content
-  `plain_text` now applies to file and pattern input as well
-  the CLI now reads from stdin, and `--text` returns the plain text version
-  the CLI now reports an error if `--pattern` is used without `--destination`
-  corrected the CLI help

## 1.5.5

-  updated to DartSass 1.89.0
