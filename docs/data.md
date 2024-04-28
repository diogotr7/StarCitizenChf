# Data

| Indices | size | data        | description         | notes                           |
| ------- | ---- | ----------- | ------------------- | ------------------------------- |
| 00-03   | 4    | 02-00-00-00 | version? Unknown    |                                 |
| 04-07   | 4    | 07-00-00-00 | CHF version.        |                                 |
| 08-0B   | 4    | see below   | Gender specific A 1 | These could be key-value pairs? |
| 0C-0F   | 4    | see below   | Gender specific A 2 |                                 |
| 10-13   | 4    | see below   | Gender specific A 3 |                                 |
| 14-17   | 4    | see below   | Gender specific A 4 |                                 |
| 18-1B   | 4    | 00-00-00-00 | Unknown             |                                 |
| 1C-1F   | 4    | 00-00-00-00 | Unknown             |                                 |
| 20-23   | 4    | 00-00-00-00 | Unknown             |                                 |
| 24-27   | 4    | 00-00-00-00 | Unknown             |                                 |
| 28-2B   | 4    | D8-00-00-00 | Key? Unknown        |                                 |
| 2C-2F   | 4    | 00-00-00-00 | Unknown             |                                 |
| 30-33   | 4    | 94-93-D0-FC | Key? Unknown        |                                 |
| 34-37   | 4    | see below   | Gender specific B 1 |                                 |
| 38-3B   | 4    | see below   | Gender specific B 2 |                                 |
| 3C-3F   | 4    | 00-00-00-00 | Unknown             |                                 |
| 40-43   | 4    | 0C-00-04-00 | Key? Unknown        |                                 |
| 44-47   | 4    | see below   | Unknown             |                                 |

## Notes

### Unknown Version

I tried changing the `2` value to `1` and `3`, this makes the game silently fail to load the character.

### CHF Version

When changing the number in the file to 0x08, this is shown in the log:

`[Error] <Unrecognised Custom Head File Version: %u> assert(false): Unrecognised Custom Head File Version: 8 [Team_S42Features][Assert]`

### Gender specific A

four 4-byte values. Always corresponds with gender.

Man: `61-4A-6B-14|D5-39-F4-25|49-8A-B6-DF|86-A4-99-A9`

Woman: `AD-4C-B0-EF|94-4A-79-D0|53-C2-D3-B4|58-25-38-AD`
  
### Gender specific B

two 4-byte values. Always corresponds with gender.

Man: `F6-67-6C-DD|D3-40-E7-65`

Woman: `54-EB-F4-9E|04-52-D7-65`

I changed these values on a default male character to the female ones. The character loads correctly and it looks pretty similar to the default male,
but the color of the head is different and shinier somehow.

### Unknown 0x40

I'm unsure if this is a value with key `0C-00-04-00` or something else entirely. Could even be a sequence of 16-bit integers since that seems to always fit rather well. Here are some values observed in this position:

women: `04-00-2B-00|04-00-2A-00|04-00-29-00|04-00-28-00|04-00-0E-00|...`

men:   `04-00-21-00|04-00-1E-00|04-00-1D-00|04-00-1C-00|04-00-1B-00|...`
