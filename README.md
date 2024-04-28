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

Read the [documentation](docs/) for more information.
