# Star Citizen .chf File Format

Stands for Character Header File. Used for storing character data in Star Citizen.

## File Format

Files are always 4096 bytes long. Any references to "always" should be interpreted as "always in the files I tested with", which were downloaded from star-citizen-characters.com

| Hex indices | size | description                               |
|-------------|------|-------------------------------------------|
| 0x000-0x003 | 4    | sq42 magic bytes (0x42, 0x42, 0x00, 0x00) |
| 0x004-0x007 | 4    | ~CRC32 checksum of data[16..]             |
| 0x008-0x00B | 4    | Compressed data size, int32               |
| 0x00C-0x00F | 4    | Decompressed data size, int32             |
| 0x010-0x013 | 4    | Zstd magic bytes (0x28, 0xB5, 0x2F, 0xFD) |
| 0x014-0xFF7 | 4072 | Data. Encrypted, then zstd compressed     |
| 0xFF8-0xFFF | 8    | Footer. More info below.                  |

### Footer

Perhaps the footer is a save date of some sort. Given the same character data, the footer changes. of course, this makes the crc also change.
Finding the purpose of the footer would be good. As far as I can tell, it is not compressed.
The first byte is always 0xF8, the last two are always 0x00.

## Encryption

Any information in this section is based on the decompressed data. It has nothing to do with the above table.

The following bytes are always the same in all files I tested with:

Common indices:
`
0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F,
0x30, 0x31, 0x32, 0x33, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x47,
0x4B, 0x4F, 0x53, 0x57, 0x5B, 0x5F, 0x63, 0x67, 0x6B, 0x6F, 0x73, 0x77, 0x7B, 0x7F, 0x83, 0x87,
0x8B, 0x8F, 0x93, 0x97, 0x9B, 0x9F, 0xA3, 0xA7, 0xAB, 0xAF, 0xB3, 0xB7, 0xBB, 0xBF, 0xC3, 0xC7,
0xCB, 0xCF, 0xD3, 0xD7, 0xDB, 0xDF, 0xE3, 0xE7, 0xEB, 0xEF, 0xF3, 0xF7, 0xFB, 0xFF, 0x103, 0x107,
0x109, 0x10A, 0x10B, 0x10C, 0x10D, 0x10E, 0x10F, 0x110, 0x111, 0x112, 0x113, 0x114, 0x115, 0x116,
0x117, 0x118, 0x119, 0x11A, 0x11B, 0x11C, 0x11D, 0x11E, 0x11F, 0x120, 0x121, 0x122, 0x123, 0x124,
0x125, 0x126, 0x127, 0x128, 0x129, 0x12A, 0x12B, 0x12C, 0x12D, 0x12E, 0x12F, 0x130, 0x131, 0x132,
0x133, 0x134, 0x135, 0x136, 0x137, 0x138, 0x139, 0x13A, 0x13B, 0x13C, 0x13D, 0x13E, 0x13F, 0x141,
0x142, 0x143, 0x144, 0x145, 0x146, 0x147, 0x148, 0x149, 0x14A, 0x14B, 0x14C, 0x14D, 0x14E, 0x14F,
0x150, 0x151, 0x152, 0x153, 0x154, 0x155, 0x156, 0x157, 0x158, 0x159, 0x15A, 0x15B, 0x15C, 0x15D,
0x15E, 0x15F, 0x160, 0x161, 0x162, 0x163, 0x164, 0x165, 0x166, 0x167, 0x179, 0x17A, 0x17B, 0x17C,
0x17D, 0x17E, 0x17F, 0x194, 0x195, 0x196, 0x197, 0x199, 0x19A, 0x19B, 0x48D, 0x49B, 0x4A2, 0x4B0
`

Values at common indices:
`
0x02, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x94, 0x93, 0xD0, 0xFC, 0x65, 0x00, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x04, 0x00, 0x04, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAC, 0x41, 0x63, 0xAB, 0x04, 0x41, 0x5F, 0x75, 0x7D,
0x8A, 0xAA, 0xDB, 0xF6, 0x76, 0x1E, 0xFD, 0x58, 0x7B, 0x24, 0x8B, 0x01, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0xB9, 0x0D, 0x01, 0x47, 0x50, 0x45, 0x80, 0xBF, 0xB3, 0xFA, 0x5C, 0x1D, 0x6E,
0x08, 0xA7, 0x96, 0xE8, 0x39, 0xAB, 0xB4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x50, 0x55,
0xBB, 0xC5, 0x71, 0x48, 0x60, 0xE1, 0x63, 0xA3, 0x4C, 0x6B, 0x10, 0x03, 0xB4, 0x67, 0x74, 0xE4,
0x09, 0xB7, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x95, 0x1A, 0x60, 0x13, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
`

Ranges:
`
0x0000-0x0007, 0x0018-0x0033, 0x003B-0x0045, 0x0047-0x0047, 0x004B-0x004B, 0x004F-0x004F, 0x0053-0x0053, 0x0057-0x0057,
0x005B-0x005B, 0x005F-0x005F, 0x0063-0x0063, 0x0067-0x0067, 0x006B-0x006B, 0x006F-0x006F, 0x0073-0x0073, 0x0077-0x0077,
0x007B-0x007B, 0x007F-0x007F, 0x0083-0x0083, 0x0087-0x0087, 0x008B-0x008B, 0x008F-0x008F, 0x0093-0x0093, 0x0097-0x0097,
0x009B-0x009B, 0x009F-0x009F, 0x00A3-0x00A3, 0x00A7-0x00A7, 0x00AB-0x00AB, 0x00AF-0x00AF, 0x00B3-0x00B3, 0x00B7-0x00B7,
0x00BB-0x00BB, 0x00BF-0x00BF, 0x00C3-0x00C3, 0x00C7-0x00C7, 0x00CB-0x00CB, 0x00CF-0x00CF, 0x00D3-0x00D3, 0x00D7-0x00D7,
0x00DB-0x00DB, 0x00DF-0x00DF, 0x00E3-0x00E3, 0x00E7-0x00E7, 0x00EB-0x00EB, 0x00EF-0x00EF, 0x00F3-0x00F3, 0x00F7-0x00F7,
0x00FB-0x00FB, 0x00FF-0x00FF, 0x0103-0x0103, 0x0107-0x0107, 0x0109-0x013F, 0x0141-0x0167, 0x0179-0x017F, 0x0194-0x0197,
0x0199-0x019B, 0x048D-0x048D, 0x049B-0x049B, 0x04A2-0x04A2, 0x04B0-0x04B0
`

| Indices                | size | data                   | description            |
|------------------------|------|------------------------|------------------------|
| 0x00, 0x01, 0x02, 0x03 | 4    | 0x02, 0x00, 0x00, 0x00 | Unknown1               |
| 0x04, 0x05, 0x06, 0x07 | 4    | 0x07, 0x00, 0x00, 0x00 | CHF version. See below |
| 0x08 - 0x17            | 16   | 0x??                   | Unknown2               |
| 0x18 - 0x27            | 16   | 0x00                   | Padding                |

Unknown1:
I tried changing the 2 value to 1 and 3, this makes the game silently fail to load the character.

CHF version:
When changing the number in the file to 0x08, this is shown in the log:
`[Error] <Unrecognised Custom Head File Version: %u> assert(false): Unrecognised Custom Head File Version: 8 [Team_S42Features][Assert]`

## Binary Protocol

### Eye color

The game has this color picker:

![Eye color picker](img/eye_picker.png)

The color is stored in the file as 4 bytes, RGBA.
In the files I have this color seems to always be 136 bytes from the end of the binary data. Maybe it's a good idea to start reverse engineering the files backwards.

color format: 0xRR, 0xGG, 0xBB, 0xAA

This table uses reverse indices, so the first byte is the last byte in the file.

| Indices         | size | description | default                    | notes                                                                 |
|-----------------|------|-------------|----------------------------|-----------------------------------------------------------------------|
| 00-01-02-03     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 04-05-06-07     | 4    | limb color  | -                          |                                                                       |
| 08-09-0A-0B     | 4    | Unknown     | BD-53-07-97                |                                                                       |
| 0C-0D-0E-0F     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 10-11-12-13     | 4    | Unknown     | 00-00-00-01                |                                                                       |
| 14-15-16-17     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 18-19-1A-1B     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1C-1D-1E-1F     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 20-21-22-23     | 4    | Unknown     | A4-1F-A1-2C or 8A-5B-66-DB | *1                                                                    |
| 24-25-26-27     | 4    | Unknown     | 00-00-00-05                |                                                                       |
| 28-29-2A-2B     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 2C-2D-2E-2F     | 4    | Torso color | -                          |                                                                       |
| 30-31-32-33     | 4    | Unknown     | BD-53-07-97                |                                                                       |
| 34-35-36-37     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 38-39-3A-3B     | 4    | Unknown     | 00-00-00-01                |                                                                       |
| 3C-3D-3E-3F     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 40-41-42-43     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 44-45-46-47     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 48-49-4A-4B     | 4    | Unknown     | 73-C9-79-A9 or 31-6B-6E-4C | seems to always match up with *1, both left or both right value       |
| 4C-4D-4E-4F     | 4    | Unknown     | 00-00-00-05                |                                                                       |
| 50-51-52-53     | 4    | Unknown     | 00-00-00-02                |                                                                       |
| 54-55-56-57     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 58-59-5A-5B     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 5C-5D-5E-5F     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 60-61-62-63     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 64-65-66-67     | 4    | Unknown     | 61-B3-1B-9A or DB-FE-C5-7E | this and the 4 next ones are always matching.                         |
| 68-69-6A-6B     | 4    | Unknown     | BF-36-02-DC or 8C-06-53-BF | either always left or always right in any given file                  |
| 6C-6D-6E-6F     | 4    | Unknown     | 98-19-1B-2D or 98-CF-80-A5 |                                                                       |
| 70-71-72-73     | 4    | Unknown     | FA-50-42-A3 or F0-15-32-62 |                                                                       |
| 74-75-76-77     | 4    | Unknown     | 85-68-48-F5 or 58-8D-4A-E8 |                                                                       |
| 78-79-7A-7B     | 4    | Unknown     | 27-42-4D-58                |                                                                       |
| 7C-7D-7E-7F     | 4    | Unknown     | 00-00-00-05                |                                                                       |
| 80-81-82-83     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 84-85-86-87     | 4    | eye color   | -                          |                                                                       |
| 88-89-8A-8B     | 4    | Unknown     | 44-2A-34-AC                |                                                                       |
| 8C-8D-8E-8F     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 90-91-92-93     | 4    | Unknown     | 00-00-00-01                |                                                                       |
| 94-95-96-97     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 98-99-9A-9B     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 9C-9D-9E-9F     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| A0-A1-A2-A3     | 4    | Unknown     | 97-36-C4-4B                |                                                                       |
| A4-A5-A6-A7     | 4    | Unknown     | 00-00-00-05                |                                                                       |
| A8-A9-AA-AB     | 4    | Unknown     | 00-00-00-01                |                                                                       |
| AC-AD-AE-AF     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| B0-B1-B2-B3     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| B4-B5-B6-B7     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| B8-B9-BA-BB     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| BC-BD-BE-BF     | 4    | Unknown     | CE-9D-F0-55                |                                                                       |
| C0-C1-C2-C3     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| C4-C5-C6-C7     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| C8-C9-CA-CB     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| CC-CD-CE-CF     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| D0-D1-D2-D3     | 4    | Unknown     | A0-47-88-5E                |                                                                       |
| D4-D5-D6-D7     | 4    | Unknown     | 00-00-00-05                |                                                                       |
| D8-D9-DA-DB     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| DC-DD-DE-DF     | 4    | Unknown     | -                          | color, probably hair. Dye? prob root                                  |
| E0-E1-E2-E3     | 4    | Unknown     | A2-C7-C9-09                |                                                                       |
| E4-E5-E6-E7     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| E8-E9-EA-EB     | 4    | Unknown     | -                          | color, hair dye most likely. not root                                 |
| EC-ED-EE-EF     | 4    | Unknown     | 15-E9-08-14                |                                                                       |
| F0-F1-F2-F3     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| F4-F5-F6-F7     | 4    | Unknown     | 00-00-00-02                |                                                                       |
| F8-F9-FA-FB     | 4    | Unknown     | 00-00-00-00                |                                                                       |
| FC-FD-FE-FF     | 4    | Unknown     | 00-00-00-00                | sometimes has data. unknown 3D-95-ED-37 / 3D-AE-EA-16 / 3A-96-FE-B4   |
| 100-101-102-103 | 4    | Unknown     | 02-7E-B6-74                |                                                                       |
| 104-105-106-107 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 108-109-10A-10B | 4    | Unknown     | ??-??-??-??                | 0-100 slider. unknown which                                           |
| 10C-10D-10E-10F | 4    | Unknown     | A5-9A-A7-C8                |                                                                       |
| 110-111-112-113 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 114-115-116-117 | 4    | Unknown     | ??-??-??-??                | 0-100 slider. unknown which                                           |
| 118-119-11A-11B | 4    | Unknown     | 06-08-40-76                |                                                                       |
| 11C-11D-11E-11F | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 120-121-122-123 | 4    | Unknown     | ??-??-??-??                | 0-100 slider. unknown which                                           |
| 124-125-126-127 | 4    | Unknown     | 62-FB-F0-AF                |                                                                       |
| 128-129-12A-12B | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 12C-12D-12E-12F | 4    | Unknown     | ??-??-??-??                | 0-100 slider. unknown which                                           |
| 130-131-132-133 | 4    | Unknown     | B9-FA-00-A3                |                                                                       |
| 134-135-136-137 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 138-139-13A-13B | 4    | Unknown     | ??-??-??-??                | 0-100 slider. unknown which                                           |
| 13C-13D-13E-13F | 4    | Unknown     | C3-37-0B-D9                |                                                                       |
| 140-141-142-143 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 144-145-146-147 | 4    | Unknown     | ??-??-??-??                | 0-100 slider. unknown which                                           |
| 148-149-14A-14B | 4    | Unknown     | 4A-F6-C1-5A                |                                                                       |
| 14C-14D-14E-14F | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 150-151-152-153 | 4    | Unknown     | 00-00-00-07                |                                                                       |
| 154-155-156-157 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 158-159-15A-15B | 4    | Unknown     | ??-??-??-??                | NOT a slider. unknown                                                 |
| 15C-15D-15E-15F | 4    | Unknown     | 00-00-00-05                |                                                                       |
| 160-161-162-163 | 4    | Unknown     | 00-00-00-01                |                                                                       |
| 164-165-166-167 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 168-169-16A-16B | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 16C-16D-16E-16F | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 170-171-172-173 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 174-175-176-177 | 4    | Unknown     | ??-??-??-??                | NOT a slider. unknown. some sequences common. hairstyle? beard?       |
| 178-179-17A-17B | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 17C-17D-17E-17F | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 180-181-182-183 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 184-185-186-187 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 188-189-18A-18B | 4    | Unknown     | 07-8A-C8-BD                |                                                                       |
| 18C-18D-18E-18F | 4    | Unknown     | 00-00-00-05                |                                                                       |
| 190-191-192-193 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 194-195-196-197 | 4    | Unknown     | -                          | color, unknown  (hair root dye)                                       |
| 198-199-19A-19B | 4    | Unknown     | A2-C7-C9-09                |                                                                       |
| 19C-19D-19E-19F | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1A0-1A1-1A2-1A3 | 4    | Unknown     | -                          | color, unknown (hair dye not-root)                                    |
| 1A4-1A5-1A6-1A7 | 4    | Unknown     | 15-E9-08-14                |                                                                       |
| 1A8-1A9-1AA-1AB | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1AC-1AD-1AE-1AF | 4    | Unknown     | 00-00-00-02                |                                                                       |
| 1B0-1B1-1B2-1B3 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1B4-1B5-1B6-1B7 | 4    | Unknown     | ??-??-??-??                | 0-100 slider?. unknown which                                          |
| 1B8-1B9-1BA-1BB | 4    | Unknown     | 02-7E-B6-74                |                                                                       |
| 1BC-1BD-1BE-1BF | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1C0-1C1-1C2-1C3 | 4    | Unknown     | ??-??-??-??                | 0-100 slider?. unknown which                                          |
| 1C4-1C5-1C6-1C7 | 4    | Unknown     | A5-9A-A7-C8                |                                                                       |
| 1C8-1C9-1CA-1CB | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1CC-1CD-1CE-1CF | 4    | Unknown     | ??-??-??-??                | 0-100 slider?. unknown which                                          |
| 1D0-1D1-1D2-1D3 | 4    | Unknown     | 06-08-40-76                |                                                                       |
| 1D4-1D5-1D6-1D7 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1D8-1D9-1DA-1DB | 4    | Unknown     | ??-??-??-??                | 0-100 slider?. unknown which                                          |
| 1DC-1DD-1DE-1DF | 4    | Unknown     | 62-FB-F0-AF                |                                                                       |
| 1E0-1E1-1E2-1E3 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1E4-1E5-1E6-1E7 | 4    | Unknown     | ??-??-??-??                | 0-100 slider?. unknown which                                          |
| 1E8-1E9-1EA-1EB | 4    | Unknown     | B9-FA-00-A3                |                                                                       |
| 1EC-1ED-1EE-1EF | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1F0-1F1-1F2-1F3 | 4    | Unknown     | ??-??-??-??                | 0-100 slider?. unknown which                                          |
| 1F4-1F5-1F6-1F7 | 4    | Unknown     | C3-37-0B-D9                |                                                                       |
| 1F8-1F9-1FA-1FB | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 1FC-1FD-1FE-1FF | 4    | Unknown     | ??-??-??-??                | 0-100 slider?. unknown which                                          |
| 200-201-202-203 | 4    | Unknown     | 4A-F6-C1-5A                |                                                                       |
| 204-205-206-207 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 208-209-20A-20B | 4    | Unknown     | 00-00-00-07                |                                                                       |
| 20C-20D-20E-20F | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 210-211-212-213 | 4    | Unknown     | ??-??-??-??                | NOT a slider. unknown. some sequences common. hairstyle? beard?       |
| 214-215-216-217 | 4    | Unknown     | 00-00-00-05                |                                                                       |
| 218-219-21A-21B | 4    | Unknown     | 00-00-00-01                |                                                                       |
| 21C-21D-21E-21F | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 220-221-222-223 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 224-225-226-227 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 228-229-22A-22B | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 22C-22D-22E-22F | 4    | Unknown     | ??-??-??-??                | NOT a slider. unknown. some sequences common. hairstyle? beard?       |
| 230-231-232-233 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 234-235-236-237 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 238-239-23A-23B | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 23C-23D-23E-23F | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 240-241-242-243 | 4    | Unknown     | 9B-27-4D-93 or 6C-83-69-47 | *2 note: these might match up with pairs above but i haven't checked. |
| 244-245-246-247 | 4    | Unknown     | 00-00-00-05                |                                                                       |
| 248-249-24A-24B | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 24C-24D-24E-24F | 4    | Unknown     | -                          | color, probably hair. beard dye?                                      |
| 250-251-252-253 | 4    | Unknown     | A2-C7-C9-09 or 68-DB-EC-22 | matched to *2                                                         |
| 254-255-256-257 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 258-259-25A-25B | 4    | Unknown     | -                          | color, hair again???                                                  |
| 25C-25D-25E-25F | 4    | Unknown     | 15-E9-08-14 or 7B-8B-1F-D6 | not matched up                                                        |
| 260-261-262-263 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 264-265-266-267 | 4    | Unknown     | 00-00-00-02                | if this is 08 there is likely an extra field after this.              |
| 268-269-26A-26B | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 26C-26D-26E-26F | 4    | Unknown     | ??-??-??-??                | sometimes a slider                                                    |
| 270-271-272-273 | 4    | Unknown     | 02-7E-B6-74                |                                                                       |
| 274-275-276-277 | 4    | Unknown     | 00-00-00-00                |                                                                       |
| 278-279-27A-27B | 4    | Unknown     | ??-??-??-??                | sometimes a slider                                                    |
| 27C-27D-27E-27F | 4    | Unknown     | A5-9A-A7-C8                |                                                                       |


