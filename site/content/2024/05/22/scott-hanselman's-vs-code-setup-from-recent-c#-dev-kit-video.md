---
title: "Scott Hanselman's VS Code Setup from recent C# Dev Kit Video"
description: ""
date: "2024-05-21T23:08:11.621Z"
tags: "[]"
categories: "[]"
---

I was watching Scott Hanselman's recent video on the C# Dev Kit and I noticed some of the structure of shows on podcast and code. This was in VS Code

![title of image](https://res.cloudinary.com/dfph3xsla/image/upload/f_auto,q_auto/v1/github/mfsbo/cmfutwcoyx99fpgzuppt)

Few things I noticed from this image are:

1. AppVersionInfo.cs which I recommend using within projects and I usually update it as a `postversion` npm script when i run `npm version patch` or `npm version minor` or `npm version major`. This allows smooth automation of versioning and gives your build process to take new version and you can use same to show on footer of your website or in about page of your app.

2. Constants.cs which is a good practice to keep all your constants in one file. This helps in managing constants and also helps in refactoring. No need to overkill if there are only 3 of them but a handy file.

3. Helpers.cs again is a good practice to keep all your helper functions in one file or folder. This helps in managing helper functions. Probably arrange them in folders only if they out grow. For one or two functions, a file is good enough.


The below image is even better on the code of versioning

![title of image](https://res.cloudinary.com/dfph3xsla/image/upload/f_auto,q_auto/v1/github/mfsbo/z346a8nyvei2y8k5rikj)

1. Scott is using _build with Filepath, build number and build ID within appVersionInfo class. BuildFileName is a json file which I assume he is reading something from and then may have a method that produces the version info based on these private variables. This is very practical and I would recommend this approach. In fact I would say add commit hash, time stamp with time zone of build and branch with it if possible. A lot of time it has saved me so much back and forth by seeing a screenshot from across the world and know the version branch and time of build to dig deep into tag that corresponds to semver.

2. Bottom folder with test.ps1 is interesting part. A lot of time I have seen this that scripts using powershell are placed in root. I recommend creating Infrastructure folder and then placing all scripts in it. Probably move all tests powershell script into their own folder. Please do not create folders called powershell and put all powershell scripts in it, that grouping is available via extension anyway. 

3. As time stamp on all files is nearly same except solution file it looks like Scott has cloned this repo for long time then touched the solution file in February 2024 and then recently went in to do something with dockerrun script. 

Here is another angle of this image
![title of image](https://res.cloudinary.com/dfph3xsla/image/upload/f_auto,q_auto/v1/github/mfsbo/eea4fleae4vixyidjxe1)

1. Love the icons used for readme.md, good use of powershell scripts. This headedtests and headlesstests is good one to run two separate files. From the size it looks like its just repeated code with some param. So maybe copilot can not help and parameterize the ps1 file so you can run tests in ci/cd and locally with command and params instead of two files.

2. restart.ps1 is interesting. I would recommend using a script that can restart the app for many reasons including running tests, restarting app after deployment, restarting app after configuration change etc. Most servers offer command line to restart app gracefully so I assume scott is using something in Docker to restart and then test the app.

3. Size of watch tests being bigger than headedtests and headlesstests hints that maybe watchtests.ps1 is manually run and being written and run in slightly different way. You can use any pattern here but having tests folder or testscripts folder will make search easy.bp


The aim of my post is not to ask Scott to change anything but to show how a simple project structure can be analyzed and improved. I am sure Scott has his reasons for this structure and it works for him. I am just showing how I see it and similar concepts are done in many server and client side applications.
