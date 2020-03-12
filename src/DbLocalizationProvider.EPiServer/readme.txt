# DbLocalizationProvider v6.0
===================================================

Might be annoying to see this file all the time (even after minor version upgrade), but it's worth reading sometimes.

Welcome to the next major version of localization provider library!
There are few breaking changes and also new features and lot's of bug fixes here and there.
Please visit this link (https://bit.ly/epi-loc-provider-v6) to read more.


## Upgrading from v5.x
If you are upgrading from v5.x -> then most common thing you are missing is storage provider configuration.
Install SQL Server storage provider by executing this in Package Manager:

```
PM> Install-Package LocalizationProvider.Storage.SqlServer
```

After you have installed SQL Server provider, add it to the configuration via extension method on `ConfigurationContext`.


Thanks You so much for the support and happy localizing!

==
Valdis Iljuconoks (aka Technical Fellow, https://tech-fellow.net)

.. Greetings from Riga (Latvia)
