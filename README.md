# SecretSanta
This application was built in 2017 quickly to facilitate a secret santa party.  Was built with Visual Studio 2017.

Once set up correctly, it will randomize the list of participants you have created, and text everyone on this list who they have to get a gift.
It will find pairings where two people will not get eachother and a participant will not get themselves.

#Setup
A couple of manual things need to be done in the code to get it to work.  Within the Send method in the SendMail class, 
there needs to be a gmail account and updated username and password for the messages to come from that email.

You will also have to set up the list of participants and their numbers.  I would like to get this into an import.  Didn't have time when I was first writing this.  Wrote it in an hour or so.
