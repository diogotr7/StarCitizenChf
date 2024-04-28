# Keys

The file seems to use a key-value system to store data. The keys are 4 bytes long. Here are some keys I was able to find:

## Reverse-engineered keys

| Key         | Value                           |
| ----------- | ------------------------------- |
| 09-C9-C7-A2 | Hair dye? def a color for sure. |
| BD-53-07-97 | skin color? def a color.        |
| 58-CB-61-93 | freckle amount                  |
| 0F-D2-4A-55 | freckle opacity                 |
| 64-12-C4-CF | sunspot amount                  |
| B0-83-58-B9 | sunspot opacity                 |

## Keys from data

These keys were found just by analyzing the data.

* 74-B6-7E-02 - not present in bruce_w, who is bald. hair?

## Keys from disassembly

Do they appear in the data?

Note: i'm analyzing the data aligned to 4 bytes, which means sometimes these won't be found even though they are present.

I don't feel like dealing with misalignment right now. :(

* 09-C9-C7-A2 yes
* 10-1B-4F-A3 no
* 14-08-E9-15 yes
* 2B-1E-DE-72 no
* 2C-09-B0-4B yes
* 2D-F4-3E-2C yes
* 2E-E5-D4-07 no
* 2F-0E-23-E3 yes
* 34-1D-DD-E1 yes
* 35-E0-53-86 yes
* 36-E7-C0-2E yes
* 37-0A-2F-47 no
* 39-17-E0-BA no
* 4A-C4-4F-9F no
* 53-93-C3-AD no
* 56-D0-BE-AA no
* 58-CD-71-57 no
* 63-C8-E0-86 no
* 77-6B-AB-A2 no
* 7F-DC-11-B3 no
* 90-1D-9B-B2 yes
* 92-E7-86-7D yes
* 93-1A-08-1A yes
* 97-07-53-BD yes
* 99-5F-96-2C no
* AC-34-2A-44 yes
