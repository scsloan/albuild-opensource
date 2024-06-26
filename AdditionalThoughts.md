
## Prereq

- Visual Studio (VS) - Community Edition will work just fine. 
- .Net 8 installed - This will come with the VS install
- Git for Windows - This can come from the VS install or you can install it manually from [Git - Downloading Package (git-scm.com)](https://git-scm.com/download/win)
## Setup 

- Download or clone the Repo

## The "AL Build" method

1. Your "Script" is just a list of Tasks spelled out in a JSON file
	1. Examples are in the Examples folder under the ALBuild Project
2. Tasks are executed in the order they appear in the file
	1. Each task can have one or more parameters
	2. Each task has a Run() which executes that task
	3. Tasks can appear more than once, so granular tasks are often better
3. Hopefully you can compose the correct list of tasks to complete the task

## Thoughts on Specific Tasks

### DownloadSymbolsOnPrem

This is the task I created for SCS to download the symbol files from an onprem server. Important to note the hardcoded version of the packages for the application Profile.  Ideally you would want to follow the TODO Comment right above the list or pass these values in through your JSON file. If you went with the JSON file method, you would then change this script to download each Symbol package individually in your JSON file. Again, granular tasks. 

## Running in Production

- Using VS, compile and build a copy of ALBuild. 
- Place the compiled binaries on the same server where the ALC.exe tools are located
- Copy over your JSON file 
- Write a Powershell script to run your newly compiled ALBuild with your json file as an argument
