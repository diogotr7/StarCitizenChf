# Star Citizen .chf File Format

Stands for Character Header File. Used for storing character data in Star Citizen.

## File Format

Files are always 4096 bytes long. Any references to "always" should be interpreted as "always in the files I tested with", which were downloaded from star-citizen-characters.com

| Hex indices | size | description                               |
| ----------- | ---- | ----------------------------------------- |
| 000-003     | 4    | sq42 magic bytes (42-42-00-00)            |
| 004-007     | 4    | ~CRC32 checksum of data[16..]             |
| 008-00B     | 4    | Compressed data size, int32               |
| 00C-00F     | 4    | Decompressed data size, int32             |
| 010-013     | 4    | Zstd magic bytes (0x28, 0xB5, 0x2F, 0xFD) |
| 014-FF7     | 4072 | Data. Encrypted, then zstd compressed     |
| FF8-FFF     | 8    | Footer. More info below.                  |

### Footer

Perhaps the footer is a save date of some sort. Given the same character data, the footer changes. of course, this makes the crc also change.
Finding the purpose of the footer would be good. As far as I can tell, it is not compressed.
The first byte is always 0xF8, the last two are always 0x00.

## Data

| Indices | size | data        | description          |
| ------- | ---- | ----------- | -------------------- |
| 00-03   | 4    | 02-00-00-00 | Some unknown version |
| 04-07   | 4    | 07-00-00-00 | CHF version.         |
| 08-17   | 16   | 0x??        | Unknown2             |
| 18-27   | 16   | 0x00        | Padding              |

### Notes

1. I tried changing the 2 value to 1 and 3, this makes the game silently fail to load the character.
2. When changing the number in the file to 0x08, this is shown in the log: `[Error] <Unrecognised Custom Head File Version: %u> assert(false): Unrecognised Custom Head File Version: 8 [Team_S42Features][Assert]`

## Data Reversed

### Table

This table uses reverse indices, so the first byte is the last byte in the file.

| Indices           | size | description | default                                   | notes                                                                                                                                                                            |
| ----------------- | ---- | ----------- | ----------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 00-01-02-03       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 04-05-06-07       | 4    | limb color  | -                                         | Sometimes opacity is zero. Color is still valid and used.                                                                                                                        |
| 08-09-0A-0B       | 4    | Unknown     | BD-53-07-97                               |                                                                                                                                                                                  |
| 0C-0D-0E-0F       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 10-11-12-13       | 4    | Unknown     | 00-00-00-01                               |                                                                                                                                                                                  |
| 14-15-16-17       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 18-19-1A-1B       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1C-1D-1E-1F       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 20-21-22-23       | 4    | Unknown     | A4-1F-A1-2C or 8A-5B-66-DB                | *1   8A-5B-66-DB = woman, A4-1F-A1-2C = man. Might be voice or smth                                                                                                              |
| 24-25-26-27       | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| 28-29-2A-2B       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 2C-2D-2E-2F       | 4    | Torso color | -                                         |                                                                                                                                                                                  |
| 30-31-32-33       | 4    | Unknown     | BD-53-07-97                               |                                                                                                                                                                                  |
| 34-35-36-37       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 38-39-3A-3B       | 4    | Unknown     | 00-00-00-01                               |                                                                                                                                                                                  |
| 3C-3D-3E-3F       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 40-41-42-43       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 44-45-46-47       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 48-49-4A-4B       | 4    | Unknown     | 73-C9-79-A9 or 31-6B-6E-4C                | seems to always match up with *1, both left or both right value                                                                                                                  |
| 4C-4D-4E-4F       | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| 50-51-52-53       | 4    | Unknown     | 00-00-00-02                               |                                                                                                                                                                                  |
| 54-55-56-57       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 58-59-5A-5B       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 5C-5D-5E-5F       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 60-61-62-63       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 64-65-66-67       | 4    | Unknown     | 61-B3-1B-9A or DB-FE-C5-7E                | this and the 4 next ones are always matching.                                                                                                                                    |
| 68-69-6A-6B       | 4    | Unknown     | BF-36-02-DC or 8C-06-53-BF                | either always left or always right in any given file                                                                                                                             |
| 6C-6D-6E-6F       | 4    | Unknown     | 98-19-1B-2D or 98-CF-80-A5                |                                                                                                                                                                                  |
| 70-71-72-73       | 4    | Unknown     | FA-50-42-A3 or F0-15-32-62                |                                                                                                                                                                                  |
| 74-75-76-77       | 4    | Unknown     | 85-68-48-F5 or 58-8D-4A-E8                |                                                                                                                                                                                  |
| 78-79-7A-7B       | 4    | Unknown     | 27-42-4D-58                               |                                                                                                                                                                                  |
| 7C-7D-7E-7F       | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| 80-81-82-83       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 84-85-86-87       | 4    | eye color   | -                                         |                                                                                                                                                                                  |
| 88-89-8A-8B       | 4    | Unknown     | 44-2A-34-AC                               |                                                                                                                                                                                  |
| 8C-8D-8E-8F       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 90-91-92-93       | 4    | Unknown     | 00-00-00-01                               |                                                                                                                                                                                  |
| 94-95-96-97       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 98-99-9A-9B       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 9C-9D-9E-9F       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| A0-A1-A2-A3       | 4    | Unknown     | 97-36-C4-4B                               |                                                                                                                                                                                  |
| A4-A5-A6-A7       | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| A8-A9-AA-AB       | 4    | Unknown     | 00-00-00-01                               |                                                                                                                                                                                  |
| AC-AD-AE-AF       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| B0-B1-B2-B3       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| B4-B5-B6-B7       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| B8-B9-BA-BB       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| BC-BD-BE-BF       | 4    | Unknown     | CE-9D-F0-55                               |                                                                                                                                                                                  |
| C0-C1-C2-C3       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| C4-C5-C6-C7       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| C8-C9-CA-CB       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| CC-CD-CE-CF       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| D0-D1-D2-D3       | 4    | Unknown     | A0-47-88-5E                               |                                                                                                                                                                                  |
| D4-D5-D6-D7       | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| D8-D9-DA-DB       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| DC-DD-DE-DF       | 4    | Unknown     | -                                         | color, probably hair. Dye? prob root                                                                                                                                             |
| E0-E1-E2-E3       | 4    | Unknown     | A2-C7-C9-09                               |                                                                                                                                                                                  |
| E4-E5-E6-E7       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| E8-E9-EA-EB       | 4    | Unknown     | -                                         | color, hair dye most likely. not root                                                                                                                                            |
| EC-ED-EE-EF       | 4    | Unknown     | 15-E9-08-14                               |                                                                                                                                                                                  |
| F0-F1-F2-F3       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| F4-F5-F6-F7       | 4    | Unknown     | 00-00-00-02 or 00-00-00-01                | in the file bruce_w this is 1 and not 2. File is shorter. character is bald. hair? the rest of this is invalid for that file                                                     |
| F8-F9-FA-FB       | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| FC-FD-FE-FF       | 4    | Unknown     | 00-00-00-00                               | 0-100 slider. unknown which                                                                                                                                                      |
| 100-101-102-103   | 4    | Unknown     | 02-7E-B6-74                               | key. not present in bruce_w. hair related?                                                                                                                                       |
| 104-105-106-107   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 108-109-10A-10B   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider. unknown which                                                                                                                                                      |
| 10C-10D-10E-10F   | 4    | Unknown     | A5-9A-A7-C8                               | key. not present in bruce_w. hair related?                                                                                                                                       |
| 110-111-112-113   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 114-115-116-117   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider. unknown which                                                                                                                                                      |
| 118-119-11A-11B   | 4    | Unknown     | 06-08-40-76                               | key. not present in bruce_w. hair related?                                                                                                                                       |
| 11C-11D-11E-11F   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 120-121-122-123   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider. unknown which                                                                                                                                                      |
| 124-125-126-127   | 4    | Unknown     | 62-FB-F0-AF                               | key. not present in bruce_w. hair related?                                                                                                                                       |
| 128-129-12A-12B   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 12C-12D-12E-12F   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider. unknown which                                                                                                                                                      |
| 130-131-132-133   | 4    | Unknown     | B9-FA-00-A3                               | key. not present in bruce_w. hair related?                                                                                                                                       |
| 134-135-136-137   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 138-139-13A-13B   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider. unknown which                                                                                                                                                      |
| 13C-13D-13E-13F   | 4    | Unknown     | C3-37-0B-D9                               | key. not present in bruce_w. hair related?                                                                                                                                       |
| 140-141-142-143   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 144-145-146-147   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider. unknown which                                                                                                                                                      |
| 148-149-14A-14B   | 4    | Unknown     | 4A-F6-C1-5A                               |                                                                                                                                                                                  |
| 14C-14D-14E-14F   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 150-151-152-153   | 4    | Unknown     | 00-00-00-07                               |                                                                                                                                                                                  |
| 154-155-156-157   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 158-159-15A-15B   | 4    | Unknown     | ??-??-??-??                               | 01-E9-09-79 or 3A-76-3F-1D or 4E-86-5B-74 or 75-19-6D-10 or 9F-37-AD-63 or EB-C7-C9-0A                                                                                           |
| 15C-15D-15E-15F   | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| 160-161-162-163   | 4    | Unknown     | 00-00-00-01                               |                                                                                                                                                                                  |
| 164-165-166-167   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 168-169-16A-16B   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 16C-16D-16E-16F   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 170-171-172-173   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 174-175-176-177   | 4    | Unknown     | ??-??-??-??                               | 23-C3-4B-C7 or 89-0C-6A-1C or 7E-06-17-A9 or D4-C9-36-72 or 52-80-CA-DF or F8-4F-EB-04 or 0F-45-96-B1 or A5-8A-B7-6A or 3B-F9-5D-6B or 91-36-7C-B0 or 17-7F-80-1D or BD-B0-A1-C6 |
| 178-179-17A-17B   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 17C-17D-17E-17F   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 180-181-182-183   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 184-185-186-187   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 188-189-18A-18B   | 4    | Unknown     | 07-8A-C8-BD                               |                                                                                                                                                                                  |
| 18C-18D-18E-18F   | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| 190-191-192-193   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 194-195-196-197   | 4    | Unknown     | -                                         | color, unknown  (hair root dye)                                                                                                                                                  |
| 198-199-19A-19B   | 4    | Unknown     | A2-C7-C9-09 or 00-00-00-00                | natercia.chf, michelangela, caldwell have this set to zeroes. All are bald. different structure from here on.                                                                    |
| 19C-19D-19E-19F   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1A0-1A1-1A2-1A3   | 4    | Unknown     | -                                         | color, unknown (hair dye not-root)                                                                                                                                               |
| 1A4-1A5-1A6-1A7   | 4    | Unknown     | 15-E9-08-14                               |                                                                                                                                                                                  |
| 1A8-1A9-1AA-1AB   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1AC-1AD-1AE-1AF   | 4    | Unknown     | 00-00-00-02                               |                                                                                                                                                                                  |
| 1B0-1B1-1B2-1B3   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1B4-1B5-1B6-1B7   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider?. unknown which                                                                                                                                                     |
| 1B8-1B9-1BA-1BB   | 4    | Unknown     | 02-7E-B6-74                               |                                                                                                                                                                                  |
| 1BC-1BD-1BE-1BF   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1C0-1C1-1C2-1C3   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider?. unknown which                                                                                                                                                     |
| 1C4-1C5-1C6-1C7   | 4    | Unknown     | A5-9A-A7-C8                               |                                                                                                                                                                                  |
| 1C8-1C9-1CA-1CB   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1CC-1CD-1CE-1CF   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider?. unknown which                                                                                                                                                     |
| 1D0-1D1-1D2-1D3   | 4    | Unknown     | 06-08-40-76                               |                                                                                                                                                                                  |
| 1D4-1D5-1D6-1D7   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1D8-1D9-1DA-1DB   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider?. unknown which                                                                                                                                                     |
| 1DC-1DD-1DE-1DF   | 4    | Unknown     | 62-FB-F0-AF                               |                                                                                                                                                                                  |
| 1E0-1E1-1E2-1E3   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1E4-1E5-1E6-1E7   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider?. unknown which                                                                                                                                                     |
| 1E8-1E9-1EA-1EB   | 4    | Unknown     | B9-FA-00-A3                               |                                                                                                                                                                                  |
| 1EC-1ED-1EE-1EF   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1F0-1F1-1F2-1F3   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider?. unknown which                                                                                                                                                     |
| 1F4-1F5-1F6-1F7   | 4    | Unknown     | C3-37-0B-D9                               |                                                                                                                                                                                  |
| 1F8-1F9-1FA-1FB   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 1FC-1FD-1FE-1FF   | 4    | Unknown     | ??-??-??-??                               | 0-100 slider?. unknown which                                                                                                                                                     |
| 200-201-202-203   | 4    | Unknown     | 4A-F6-C1-5A                               |                                                                                                                                                                                  |
| 204-205-206-207   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 208-209-20A-20B   | 4    | Unknown     | 00-00-00-07                               |                                                                                                                                                                                  |
| 20C-20D-20E-20F   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 210-211-212-213   | 4    | Unknown     | ??-??-??-??                               | 0D-4B-69-54 or 0E-6B-A1-FA or 13-17-0B-02 or **        Might be the hairstyle, low confidence                                                                                    |
| 214-215-216-217   | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| 218-219-21A-21B   | 4    | Unknown     | 00-00-00-01                               |                                                                                                                                                                                  |
| 21C-21D-21E-21F   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 220-221-222-223   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 224-225-226-227   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 228-229-22A-22B   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 22C-22D-22E-22F   | 4    | Unknown     | ??-??-??-??                               | NOT a slider. unknown. some sequences common. hairstyle? beard?                                                                                                                  |
| 230-231-232-233   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 234-235-236-237   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 238-239-23A-23B   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 23C-23D-23E-23F   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 240-241-242-243   | 4    | Unknown     | 9B-27-4D-93 or 6C-83-69-47                | these might match up with pairs above but i haven't checked. NOT gender                                                                                                          |
| 244-245-246-247   | 4    | Unknown     | 00-00-00-05                               |                                                                                                                                                                                  |
| 248-249-24A-24B   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 24C-24D-24E-24F   | 4    | Unknown     | -                                         | if next int is A2-C7-C9-09, this is a color color, probably hair. beard dye? if next int is 68-DB-EC-22 or 00-00-00-00, this is also 00-00-00-00                                 |
| 250-251-252-253   | 4    | Unknown     | A2-C7-C9-09 or 68-DB-EC-22 or 00          | sequences of bytes, usually those two but sometimes zero. not 1-1 to the previous                                                                                                |
| 254-255-256-257   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 258-259-25A-25B   | 4    | Unknown     | -                                         | color, hair again???                                                                                                                                                             |
| 25C-25D-25E-25F   | 4    | Unknown     | 15-E9-08-14 or 7B-8B-1F-D6 or 9B-58-9C-4F | not matched up                                                                                                                                                                   |
| 260-261-262-263   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 264-265-266-267   | 4    | Unknown     | 00-00-00-02                               | if this is 08 there is likely an extra field after this.                                                                                                                         |
| 268-269-26A-26B   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 26C-26D-26E-26F   | 4    | Unknown     | ??-??-??-??                               | sometimes a slider                                                                                                                                                               |
| 270-271-272-273   | 4    | Unknown     | 02-7E-B6-74                               |                                                                                                                                                                                  |
| 274-275-276-277   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 278-279-27A-27B   | 4    | Unknown     | ??-??-??-??                               | sometimes a slider                                                                                                                                                               |
| 27C-27D-27E-27F   | 4    | Unknown     | A5-9A-A7-C8                               |                                                                                                                                                                                  |
| 280-281-282-283   | 4    | Unknown     | 00-00-00-00                               |                                                                                                                                                                                  |
| 284-285-286-287   | 4    | Unknown     | -                                         | color, unknown                                                                                                                                                                   |
| ................. |      |             |                                           |                                                                                                                                                                                  |
| 348-349-34A-34B   | 4    | Unknown     | 00-00-00-00                               | Face color                                                                                                                                                                       |

### Eye color

The game has this color picker:

![Eye color picker](img/eye_picker.png)

The color is stored in the file as 4 bytes, RGBA.
In the files I have this color seems to always be 136 bytes from the end of the binary data. Maybe it's a good idea to start reverse engineering the files backwards.

color format: 0xRR, 0xGG, 0xBB, 0xAA

## Theory

| Key         | Value                           |
| ----------- | ------------------------------- |
| A2-C7-C9-09 | Hair dye? def a color for sure. |
| BD-53-07-97 | skin color? def a color.        |
| 93-61-cb-58 | freckle amount                  |
| 55-4a-d2-0f | freckle opacity                 |
| cf-c4-12-64 | sunspot amount                  |
| b9-58-83-b0 | sunspot opacity                 |

It's also possible that A2-C7-* refers to a color data type.

### Keys from disassembly

* 07-D4-E5-2E
* 15-E9-08-14
* 1A-08-1A-93
* 2C-3E-F4-2D
* 2C-96-5F-99
* 2E-C0-E7-36
* 44-2A-34-AC
* 47-2F-0A-37
* 4B-B0-09-2C
* 57-71-CD-58
* 72-DE-1E-2B
* 7D-86-E7-92
* 86-53-E0-35
* 86-E0-C8-63
* 9F-4F-C4-4A
* A2-AB-6B-77
* A2-C7-C9-09
* A3-4F-1B-10
* AA-BE-D0-56
* AD-C3-93-53
* B2-9B-1D-90
* B3-11-DC-7F
* BA-E0-17-39
* BD-53-07-97
* E1-DD-1D-34
* E3-23-0E-2F

#### Keys from data

* 02-7E-B6-74 - not present in bruce_w, who is bald. hair?


#### Percent values (approximate)

* 000% -> 00000000 = 0
* 000% -> 3bd5a73a = 1000000000
* 000% -> 3bd5a73a = 1003857722
* 000% -> 3be6fdba = 1004993978
* 000% -> 3bebf766 = 1005320038
* 001% -> 3c8896ef = 1015584495
* 002% -> 3cda72dd = 1020949213
* 003% -> 3d0f9919 = 1024432409
* 004% -> 3d3fb1ae = 1027584430
* 050% -> 3f000000 = 1056964608
* 051% -> 3f04985e = 1057265758
* 052% -> 3f077598 = 1057453464
* 053% -> 3f08a29c = 1057530524
* 096% -> 3f78412f = 1064845615
* 097% -> 3f795920 = 1064917280
* 098% -> 3f7d35c8 = 1065170376
* 099% -> 3f7e5178 = 1065243000
* 100% -> 3f800000 = 1065353216


max - min = 1065353216 - 1000000000 = 65353216
formula 0-100 -> hex = 1000000000 + (percentage * 65353216 / 100)

^ after i did all that i figured out it's just a big endian 32 bit float lol
