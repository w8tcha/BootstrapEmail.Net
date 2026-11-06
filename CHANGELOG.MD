## 2.0.0

-  Sass is now compiled in managed code with DartSassHost and Jint, so no platform specific Dart Sass runtime is needed
-  the default stylesheets are included precompiled, only custom Sass gets compiled
-  **Breaking:** removed `ConfigStore.DartSassNativeType`, and the `DartSass.Native.*` packages are no longer used
-  **Breaking:** `StyleType` is now `BootstrapEmail.Net.StyleType` instead of the Citizen17.DartSass type

## 1.5.5

-  updated to DartSass 1.89.0
