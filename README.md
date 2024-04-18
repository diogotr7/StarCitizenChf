# Star Citizen .chf File Format

Stands for Character Header File. Used for storing character data in Star Citizen.

## File Format

Files are always 4096 bytes long. These indices are always the same for all files I tested with:
`0x00, 0x01, 0x02, 0x03, 0x0A, 0x0B, 0x0E, 0x0F, 0x10, 0x11, 0x12, 0x13, 0x14, 0x19, 0xFF8, 0xFFE, 0xFFF`

And these are the values at those indices:

`0x42, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x28, 0xB5, 0x2F, 0xFD, 0x60, 0x00, 0xF8, 0x00, 0x00`

Any references to "always" should be interpreted as "always in the files I tested with", which were about 150.

| Hex indices                | dec indices            | size | data                   | description                                                              |
|----------------------------|------------------------|------|------------------------|--------------------------------------------------------------------------|
| 0x00, 0x01, 0x02, 0x03     | 00, 01, 02, 03         | 4    | 0x42, 0x42, 0x00, 0x00 | Header, probably sq42 reference                                          |
| 0x04, 0x05, 0x06, 0x07     | 04, 05, 06, 07         | 4    | 0x??, 0x??, 0x??, 0x?? | Unknown. Changes even if the same character is exported                  |
| 0x08, 0x09, 0x0A, 0x0B     | 08, 09, 10, 11         | 4    | 0x??, 0x??, 0x00, 0x00 | Unknown                                                                  |
| 0x0C, 0x0D, 0x0E, 0x0F     | 12, 13, 14, 15         | 4    | 0x??, 0x??, 0x00, 0x00 | Unknown                                                                  |
| 0x10, 0x11, 0x12, 0x13     | 16, 17, 18, 19         | 4    | 0x28, 0xB5, 0x2F, 0xFD | Always the same **zstd compression header**                              |
| 0x14                       | 20                     | 1    | 0x60                   | Unknown. Always the same                                                 |                                                    |
| 0x15, 0x16, 0x17, 0x18     | 21, 22, 23, 24         | 4    | 0x??, 0x??, 0x??, 0x?? | Unknown                                                                  |
| 0x19                       | 25                     | 1    | 0x00                   | Unknown. Always the same                                                 |
| 0x20 to ?                  | 26 to 4087             | 4062 | ?                      | data. Very high entropy, according to SCL this is encrypted + compressed |
| 0xFF8                      | 4088                   | 1    | 0xF8                   | Unknown. Always the same                                                 |
| 0xFF9, 0xFFA, 0xFFB, 0xFFC | 4089, 4090, 4091, 4092 | 4    | 0x??, 0x??, 0x??, 0x?? | Unknown. 0xffb is 60 in every file i have except for 1                   |
| 0xFFD                      | 4093                   | 1    | 0x??                   | Unknown                                                                  |
| 0xFFE, 0xFFF               | 4094, 4095             | 2    | 0x00, 0x00             | end                                                                      |

## Data

The data part of the file is encrypted and compressed.
Not all the bytes are used. In some files,  the leftover space is zeroed out, in others it is 0xDEADBEEF. Not sure why, possibly older builds used DEADBEEF for debugging purposes.

Right before we get to the padding, we can find something like this:

`0x??,0x00,0x00,0x00,0x00,0x00,0x00,0x00`

I am not sure what this is.