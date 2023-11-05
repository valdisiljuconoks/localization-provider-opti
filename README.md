# DbLocalizationProvider for Optimizely (ex. Episerver) CMS and Commerce

[<img src="https://tech-fellow-consulting.visualstudio.com/_apis/public/build/definitions/7cf5a00f-7a74-440c-83bd-45d6c8a80602/11/badge"/>](https://tech-fellow-consulting.visualstudio.com/localization-provider-epi/_build/index?definitionId=11)
[![Platform](https://img.shields.io/badge/Optimizely-%2012-blue.svg?style=flat)](http://world.optimizely.com/)

# v7.0 is FINALLY OUT!

Please read more in [this blog post](https://blog.tech-fellow.net/2022/01/23/dblocalizationprovider-for-optimizely/)!

## Supporting LocalizationProvider

If you find this library useful, cup of coffee at late night while library is being hacked around would be awesome! You can support further development via [Paypal](https://paypal.me/valdisiljuconoks).

## What is the LocalizationProvider project?

LocalizationProvider project is Optimizely localization provider on steriods.

Giving you main following features:
* Database driven localization provider for Optimizely websites projects
* Easy resource registrations via code (code-first apporoach)
* Supports hierarchical resource organization (with help of child classes)
* Administration UI for editors to change or add new translations for required languages

## Getting Started
* [Getting Started](docs/getting-started-epi.md)

## Migrating from v5.x?
Please visit [blog post](https://blog.tech-fellow.net/2020/02/21/localization-provider-major-6/) to get more information about new features added in v6 and also how to migrate.

## Working with DbLocalizationProvider
* For more info about generic localization provider features read more [here](https://github.com/valdisiljuconoks/LocalizationProvider/blob/master/README.md)

## Integrating with Optimizely
* [Working with Resources (Optimizely)](docs/working-with-resources-epi.md)
* [Optimizely Frontend Localization](docs/jsresourcehandler-epi.md)
* [Optimizely Xml File Migration](docs/xml-migration-epi.md)

<!-- * [Admin UI (Optimizely)](docs/adminui-epi.md) -->

# How to Contribute

It's super cool if you read this section and are interesed how to help the library. Forking and playing around sample application is the fastest way to understand how localization provider is working and how to get started.

Forking and cloning repo is first step you do. Keep in mind that provider is split into couple repositories to keep thigns separated. Additional repos are pulled in as submodules. If you Git client does not support automatic checkout of the submodules, just execute this command at the root of the checkout directory:

```
git clone --recurse-submodules git@github.com:valdisiljuconoks/localization-provider-epi.git
```

**NB!** As Optimizely repository contains 2 level (`lib/netcore` and `lib/netcore/lib/localization-provider`) submodules `--recursive-submodules` sometimes fails to detect 2nd level module. Then you can just step into `netcore` submodule folder and execute pull command there:

```
git submodule foreach git pull origin master
```

# More Info (Some blog posts)

* [Part 1: Resources and Models](http://blog.tech-fellow.net/2016/03/16/db-localization-provider-part-1-resources-and-models/)
* [Part 2: Configuration and Extensions](http://blog.tech-fellow.net/2016/04/21/db-localization-provider-part-2-configuration-and-extensions/)
* [Part 3: Import and Export](http://blog.tech-fellow.net/2017/02/22/localization-provider-import-and-export-merge/)
* [Part 4: Resource Refactoring and Migrations](https://blog.tech-fellow.net/2017/10/10/localizationprovider-tree-view-export-and-migrations/)
