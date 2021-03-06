# MyOddWeb.Classifier [![Release](https://img.shields.io/badge/release-v0.9.1.1-brightgreen.png?style=flat)](https://github.com/FFMG/myoddweb.classifier/releases/latest)

<!---
Mono does not work currently...
 ## Status
[comment]: <> [![Build Status](https://travis-ci.org/FFMG/myoddweb.classifier.svg?branch=master)](https://travis-ci.org/FFMG/myoddweb.classifier)
-->

## What is it

MyOddWeb classifier, as it's name mentions, classifies emails as they arrive and move them to predefined folders.

Traditionally, rules are used to move emails to folders, for example an email from work, would be moved to the "Work" folder.

With a classifier you can create categories, "Personal", "NSFW", "Spam" and so on, and if a mail arrives, regardless who it is from, it will be moved to the appropriate folder.

Using the "Work" example above, if Bob from Human Resources sends you a joke email, a rule would have traditionally moved that email to the "Work" folder, (as Bob is a co-worker). With a Classifier, that email would be moved to a "Joke" folder and you can then look at it later.

## How does it work

### Documentation

Visit the [wiki](https://github.com/FFMG/myoddweb.classifier/wiki).

### General

MyOddWeb is a [Naive Bayes classifier](https://en.wikipedia.org/wiki/Naive_Bayes_classifier), in simple terms, it makes an educated guess as to what category the email might belong in considering all the thousand of emails that you might have already received.

At first, you 'train' your classifier with your own "Categories", what you consider a "joke" might be considered "NSFW" by others.

The more you train it, the better the classifier gets.

But it does not take that long, really, invest a couple of day creating good Magnets and good Categories and in no time your classifier will be self sufficient.

MyOddWeb Classifier works with POP3, SMTP, NNTP proxies IMAP and exchanges.

## Setup

### From setup

- Download the setup app, (get the [latest release](https://github.com/FFMG/myoddweb.classifier/releases/latest))
  - Make sure outlook is not running.
- Run it
- Start outlook
  - You will need to accept the certificate, (only first time).
- Create some categories
- Create some magnets
- Categorise some emails.

### From the code

- Get the code
- Build it using visual studio 2017 or later, (I tried with 2015 as well ... and it works(ish)).
- You can either debug and/or simply run outlook.

## Versions

### 0.9.1.1 (10/30/2019)

- Updated to less restrictive MIT License
- Added option to re-scan entire folder, (for whatever reason)
  - Added progress bar when loading/processing large number of mail.
- Empty/no categories no longer hide the menu.
- Some code cleanup
- Updated to engine 1.7.3
  - Sqlite 3.30.1
- Various code optimisations.
- Various performance / memory fixes
- Minor other improvements

### 0.8.0.1 (09/01/2019)

- Some code cleanup
- Updated to engine 1.7.0
  - Sqlite 3.26.0

### 0.7.0.5 (09/11/2017)

- Added - Added an option to train items that are manually moved from one folder to another.
  - Also added option to ask the user if they want to select category in the case where we have more than one.
- Added - On start up we now log version number + engine number.
- Added Exception logging, (higher lever than error)
- Updated to 1.6.5 engine.
  - Sqlite 3.21.0
- Changed the way he handle emails when the wait time is 0, (we handle it right away).
- Ignored conversations no longer cause messages to be classified, (as they are normally deleted)
- Did some code cleanup
  - Changed from MSTest to NUnit, (because of Moq)
  - Create multiple interfaces for the engine.
- Fixed an issue with logging, not all types of logs were added to the db.  
- Fixed a couple of colour issues in the option dialog.

### 0.7.0.2-0.7.0.4 (04/10/2017)

- Started work on the [wiki](https://github.com/FFMG/myoddweb.classifier/wiki).

- Added Minimum category percent.
- We now 'timestamp' the last received time, that way, we can check emails that arrived while the classifier was not running, (in Exchanges for example).
  - Added CheckUnProcessedEmailsOnStartUp option in case you don't want that check, (if you are happy with the Exchange rules been applied or if you have another outlook applying rules already).
- Updated to 1.6.3 engine + interface.
- Added option to delay the classification, (to allow rules to fire).
  - Fixed a bug in the delayed emails (0.7.0.4).
- Added option to set the number of entries we want to display.
- Changed the behaviour of some option values to be read only, (you could not change the value anyway, so it was a bit misleading)
- Changed GetLogEntries( ... ) to now return the entry ID as well.
  - The log summary now has the log id in it to make it easier to find.
- Fixed an issue with the _order_ of the last 'x' log entries been displayed, (was mostly un-ordered).
- Fixed a option spelling mistake, "CheckIfUnownCategory" > "CheckIfUnKnownCategory".
- Fixed wording of "Common word percent", (functionality is ok, just wording).

#### 0.7.0.1 (17/09/2017)

- Updated to 1.6.2 engine.
- Added log options, (level + retention time)
- Added log viewer.
- Fixed setup to we don't check x64 registry.

#### 0.6.0.1 - 0.6.0.5 (20/04/2017)

- Added debug log messages to time how slow classification is.
- Updated to classifier engine 1.5.7.0, (faster)
  - Added a couple of css values to no parse, (max-width/width etc...)
- Updated the interface 0.6.0.5, (no change, just align the number)
- Fixed a couple of issues with the 'detailed' html view.
  - Speed up the way the display is created.
  - Fixed actual html created, (invalid tables and so on).
- Changed the way the interface loads Outlook folders, they are now loaded dynamically rather than at run time only.  

### 0.5.0.1 - 0.5.0.2 (06/11/2016)

- Option to automatically train classified mail, (false by default)
- Option to automatically train classified mail using magnet, (true by default)
- Updated to 1.5.2 engine, ( has better/faster classification)
  - Classifications are now threaded.
- Options text now gives the version number + engine version number.

### 0.4.0.1 - 0.4.0.2 (02/11/2016)

- Fixed a few issues in the tokenizer.
- Made some changes in the engine with the lessons learned over the last 6 months.
  - More html code is now ignored.
- We can now see the classification details to see each words classification.
  - Added a menu option to display raw-text classification.
  - Added a 'viewer' application to test raw text to confirm that the engine is working as expected.
  
### 0.3.0.5 (01/06/2016)

- Fixed a couple of EventSource issues.
- Changed the output name of the setup.exe so it includes the version number.

#### 0.3.0.4 (17/02/2016)

- Updated to new Classifier engine 1.1.0, (bug fixes)
  - New setting to remove very common words, ("Option.CommonWordsMinPercent").
- Added ***GetEngineVersion()*** to use the new engine version.  
- Added possibility to change the common word percentage.

### 0.3.0.3 (01/02/2016)

- Updated to new Classifier engine, (bug fixes)  

### 0.3.0.2 (20/01/2016)

- Removed a couple of outlook/html/xml words that could confuse the classifier.  

### 0.3.0.1 (24/12/2015)

- Fixed a typo in the name  

### 0.3.0.0 (24/12/2015)

- Added weights
- Added option to set the magnets/user specified items weight.  

### 0.2.1.0 (11/11/2015)

- Removed dependency on .NET 4.5.2 and replaced it with more realistic 4.5.0  

### 0.2.0.0 (09/11/2015)

- By default we compile the x86 and x64 binaries.
- Created 2 output folders.
- We now automatically load the x64 or x86 dll depending on the version of outlook running.

### 0.1.0.0 (05/11/2015)

- **Beta**
- Initial release

## FAQ

### General

#### What are magnets

Magnets are like 'shortcuts' for your classifier, for example, if you get an email from your mom, you can create a magnet to always classify her emails has 'Personal', this will help the classifier as well as prevent some of her mails been wrongly classified.
As a rule, the more magnet you have, the better.

**NB**: Should you add a magnet for spam emails? The short answer is no, spam emails come from thousands if not hundreds of thousands of servers. You cannot realistically create a magnet for all of them.
But you could always create one for the repeat offender, personally, I don't have one.

#### How quickly does the app learn

It is fairly quick, but you should monitor the first few days to make sure that everything is on the right path.
Eventually, it will keep on learning and you will not need to worry.

**NB**: You should always check your spam folder to make sure that nothing has been sent there by mistake.

#### How many magnets/categories can I have

There is no limit on the number of categories and/or magnets.
But you have to be realistic and not have too many.

### Weights
Weights are ways of marking an email more or less important depending on .

For example, if you use a magnet then the weight of that email is slightly more than when the engine tries to categorize the email itself.

When you categorize an email yourself, then this is considered to have the greatest weight, (who knows better than you what an email should be).

### Database

#### Where is the database

The database is located in your "**%appdata%\myoddweb\Classifier**" folder.

#### What format is the database

It is a [SQLite](https://www.sqlite.org/ "Sqlite") database

#### Can I clear my database

Just close outlook, delete the database and restart outlook.

# Todo

- Create some tutorials.
  - How to setup
  - <s>Manage categories</s>[0.5.0.4]
  - <s>Manage Magnets</s>[0.5.0.4]
  - Maybe create a youtube account and explain various features.
- Give a link to the setup program on http://www.myoddweb.com
- Update the site with more information.
- Test on versions of Outlook, please add a message if you are able to test.
  - <s>Test on Office 365</s>[0.9.1.1]
  - <s>Test on outlook 2016</s>[0.6.0.5]
  - <s>Test on outlook 2013</s>[0.5.0.4]
  - <s>Test on outlook 2010</s>[0.5.0.4]
  - Test on outlook 2007
- Test on 32bit versions of Outlook.
  - <s>On 32bit machines</s>[0.6.0.5]
  - <s>On 64bit machines</s>[0.6.0.5]
- <s>Test on 64bit versions of Outlook</s>[0.6.0.5]
- Ask someone to give us a certificate rather than the temp one we have.
- Languages
- Add copyright notice in front of all the files...

## LICENSE

[MIT License](https://github.com/FFMG/myoddweb.classifier/blob/master/LICENSE)
