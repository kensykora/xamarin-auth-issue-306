# Xamarin Issue 306 Error Reproduction

Error: https://github.com/xamarin/Xamarin.Auth/issues/306

## Environment

Tested on environment:

Samsung Galaxy S6, Android API 24. Latest updates, Chrome Browser enabled.

## Steps to reproduce

 1. Launch App
 2. Tap "Login" Button
 3. Observe that chrome custom tab browser opens
 4. Tap the "X" button in the upper left.

 **Expected**: Browser closes, the Authenticator `Completed` event occurs, with the user being not logged in.  
 **Actual**: Custom tab opens up again, same page

 **Workaround**: 5. Tap "X" in the upper left a second time. Observe that the browser closes and the Authenticator `Completed` event occurs, with the user being not logged in.