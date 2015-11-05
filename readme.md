# MyOddWeb.Classifier #

<!---
Mono does not work currently...
 ## Status ##
[comment]: <> [![Build Status](https://travis-ci.org/FFMG/myoddweb.classifier.svg?branch=master)](https://travis-ci.org/FFMG/myoddweb.classifier)
-->

## What is it ##
MyOddWeb classifier, as it's name mentions, classifies emails as they arrive and move them to predefined folders.

Traditionally, rules are used to move emails to folders, for example an email from work, would be moved to the "Work" folder.

With a classifier you can create categories, "Personal", "NSFW", "Spam" and so on, and if a mail arrives, regardless who it is from, it will be moved to the appropriate folder.

Using the "Work" example above, if Bob from Human Resources sends you a joke email, a rule would have traditionally moved that email to the "Work" folder, (as Bob is a co-worker). With a Classifier, that email would be moved to a "Joke" folder and you can then look at it later. 

## How does it work ##

MyOddWeb is a [Naive Bayes classifier](https://en.wikipedia.org/wiki/Naive_Bayes_classifier), in simple terms, it makes an educated guess as to what category the email might belong in considering all the thousand of emails that you might have already received. 

At first, you 'train' your classifier with your own "Categories", what you consider a "joke" might be considered "NSFW" by others.

The more you train it, the better the classifier gets.

But it does not take that long, really, invest a couple of day creating good Magnets and good Categories and in no time your classifier will be self sufficient. 

## Setup ##
### From setup ###

- Download the setup app, (in the [Classifier.Setup/Output](https://github.com/FFMG/myoddweb.classifier/tree/master/Classifier.Setup/Output) sub folder for now)
- Run it
- Start outlook
	- You will need to accept the certificate, (only first time).
- Create some categories
- Create some magnets
- Categorise some emails.

### From the code ###

- Get the code
- Build it using visual studio 2015 or later, (I have not tried the others).
- You can either debug and/or simply run outlook.

## Versions ##
### 0.1.0.0 (05/11/2015)###

- **Beta**
- Initial release


## FAQ ##
### General ###
#### What are magnets ####
Magnets are like 'shortcuts' for your classifier, for example, if you get an email from your mom, you can create a magnet to always classify her emails has 'Personal', this will help the classifier as well as prevent some of her mails been wrongly classified.
As a rule, the more magnet you have, the better.

**NB**: Should you add a magnet for spam emails? The short answer is no, spam emails come from thousands if not hundreds of thousands of servers. You cannot realistically create a magnet for all of them.
But you could always create one for the repeat offender, personally, I don't have one.

#### How quickly does the app learn ####
It is fairly quick, but you should monitor the first few days to make sure that everything is on the right path.
Eventually, it will keep on learning and you will not need to worry.

**NB**: You should always check your spam folder to make sure that nothing has been sent there by mistake.

#### How many magnets/categories can I have ####
There is no limit on the number of categories and/or magnets.
But you have to be realistic and not have too many.

### Database ###
#### Where is the database ####
The database is located in your "**%appdata%\myoddweb\Classifier**" folder.

#### What format is the database ####
It is a [SQLite](https://www.sqlite.org/ "Sqlite") database

#### Can I clear my database ####
Just close outlook, delete the database and restart outlook.

# Todo #

- Create some tutorials.
	- How to setup
	- Manage categories
	- Manage Magnets
	- Maybe create a youtube account and explain various features.
- Give a link to the setup program on http://www.myoddweb.com
- Update the site with more information.
- Test on versions of Outlook, please add a message if you are able to test.
	- Test on outlook 2016
	- <s>Test on outlook 2013</s>
	- Test on outlook 2010
	- Test on outlook 2007
- Test on 32bit versions of Outlook.
	- On 32bit machines
	- On 64bit machines
- Test on 64bit versions of Outlook
- Ask someone to give us a certificate rather than the temp one we have.
- Languages
- Add copyright notice in front of all the files...