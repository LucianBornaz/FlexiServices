# FlexiServices

Library which helps to refactor existing monolithic solutions and run classes as WCF services without changing the code.
Classes run as WCF services need to be moved in a separate project than the interfaces they implement.
DI container needs to be set up to create a channel to the newly created WCF service instead of an instance of the class.

Adantages:
- classes can be easily set up to be consumed directly or as WCF services;
- no need to change the code, only DI container settings;
- existing exception handling (try-catch) will work as before.
