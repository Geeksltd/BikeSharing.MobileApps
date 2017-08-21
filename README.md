# BikeRider App - based on Zebble for Xamarin

## BACKGROUND

At Microsoft Connect(); 2016 event last year, Microsoft demoed a mobile apps for BikeSharing360, a fictional company that allows users to rent bikes located throughout New York City and Seattle. BikeRider, the consumer mobile app for BikeSharing360, is an open source, beautiful native mobile app for iOS, Android, and Windows built with **Xamarin.Forms**. 

You can [learn more about it here](https://blog.xamarin.com/introducing-bikerider-app/) or check out the [Xamarin Forms version's](https://github.com/Microsoft/BikeSharing360_MobileApps) source.

## Comparison

To demonstrate the benefits of **Zebble** and provide a fair comparison with Xamarin Forms, we have implemented exactly the same design and functionality ***using Zebble instead of Xamarin Forms***.

### Comparison Result: Code Size: 1/5th !

The same app is written in Zebble with 1/5th of the code size compared to Xamarin Forms. That's over 80% saving of your time and cost, which also translates to a much better maintainability.

### Lines of code
* Xamarin Forms: ~12,000
* Zebble: ~2,700

### Another measurement: Programming Tokens
You might think, counting lines of code isn't fair, because there can be different coding styles, and liberal use of empty lines. To address that concern and neutralise coding styles, line splitting preferences, optional use of { and } characters, etc, we also count the number of actual tokens which is effectively programming keywords, identifiers, etc. The result was even beter for Zebble:

* Xamarin Forms: ~62,000 tokens
* Zebble: ~12,700 tokens

### UI Code & maintainability

The code of the Zebble version not only briefer but also cleaner, easier to write and debug. As you can see for yourself by comparing the source code of the two implementations:

* The Zebble UI styling code is in CSS, instead of hard-coded embedded resource styling you see in Xamarin Forms.
* There is almost no platform specific code in the Zebble version, contrary to the over 850 references to specific platforms (iOS, Android or Windows) in the Xamarin Forms's version.
* The mark up in Zebble (.zbl files) is much briefer, cleaner and more semanric than the XAML markup used by Xamarin Forms.
* Resources (images and other files) are centrally hosted in a single directory in Zebble, while they are duplicated 3 times in different folder structures in the Xamarin Forms version.
