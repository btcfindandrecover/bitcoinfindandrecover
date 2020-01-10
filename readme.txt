Bitcoin find & recover is a crypto currency recovery tool, that builds on top of the great btcrecover by Gurnec https://github.com/gurnec/btcrecover, to help any bitcoin user to recover by themselves their lost passwords.
Supports all desktop wallets and a few mobile. Should work for most altcoins wallets, forked from the ones listed below.

Automatizes the searching for wallet files, in files existing and deleted.
It's able to recover partially corrupted MultibitHD wallets.
It also allows the user to create a password list from whatever fragments he or she remembers, to try to regain access with a lost password.
The recovered passwords are saved encrypted using AES.

Works only on Windows and requires .NET 4.0, python 2.7 and a few other prerequisites installed by the setup.

Supports:

- Armory
- Bitcoin-QT
- Bither
- Copay
- Electrum
- mSIGNA
- Multibit
- MultibitHD

## Bitcoin wallet finder

![](https://i.imgur.com/JFhO313.png)

All files will be restored to a folder selected on the GUI. If you chose to recover deleted files, they'll be placed on a sub-folder called "RestoredTemp".
Different sub-folders will be created per wallet. Files copied from already existing files will have the directory structure copied as well. Those recovered from deletion will not, due to a limitation of the file system. Those will be placed on a sub-folder called "recovered", under each wallet's folder.

I based the deleted file recovery work on this great project: https://sourceforge.net/projects/kickassundelete/

## Bitcoin password recover

![](https://i.imgur.com/0OZdwpJ.png)

It's basically a GUI for btcrecover, which is very powerful. My idea was to help users work with it, by creating an interface to operate that program in a friendlier manner.
The most important thing to understand is how to create the password tokens, which are basically the different parts you remember of the password.
It allows to either try to recover all the files found by the first part of the program or only recover one file.

I've copied the most important sections of the token creation guide from btcrecover, see here:
https://github.com/Alex-Jaeger/BitcoinFindAndRecover/blob/master/Tokens.txt
