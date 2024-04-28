# Data

All values are in hex unless otherwise specified.

| Indices | size | data        | description         |
| ------- | ---- | ----------- | ------------------- |
| 00-03   | 4    | 02-00-00-00 | version? Unknown    |
| 04-07   | 4    | 07-00-00-00 | CHF version.        |
| 08-0B   | 4    | see below   | Gender specific A 1 |
| 0C-0F   | 4    | see below   | Gender specific A 2 |
| 10-13   | 4    | see below   | Gender specific A 3 |
| 14-17   | 4    | see below   | Gender specific A 4 |
| 18-1B   | 4    | 00-00-00-00 | Unknown             |
| 1C-1F   | 4    | 00-00-00-00 | Unknown             |
| 20-23   | 4    | 00-00-00-00 | Unknown             |
| 24-27   | 4    | 00-00-00-00 | Unknown             |
| 28-2B   | 4    | D8-00-00-00 | Key? Unknown        |
| 2C-2F   | 4    | 00-00-00-00 | Unknown             |
| 30-33   | 4    | 94-93-D0-FC | Key? Unknown        |
| 34-37   | 4    | see below   | Gender specific B 1 |
| 38-3B   | 4    | see below   | Gender specific B 2 |
| 3C-3F   | 4    | 00-00-00-00 | Unknown             |
| 40-43   | 4    | 0C-00-04-00 | DNA Data Key        |
| 44-47   | 4    | see below   | DNA Data            |
| ...     | CC   | ...         | ...                 |
| 110-113 | 4    | AC-41-63-AB | Unknown Key         |
| 114-117 | 4    | 04-41-5F-75 | Unknown Key         |
| 118-11B | 4    | 7D-8A-AA-DB | Unknown Key         |
| 11C-11F | 4    | F6-76-1E-FD | Unknown Key         |
| 120-123 | 4    | 58-7B-24-8B | Unknown Key         |
| 124-127 | 4    | 01-00-00-00 | Unknown Key         |
| 128-12B | 4    | 00-00-00-00 | Unknown Key         |
| 12C-12F | 4    | B9-0D-01-47 | Unknown Key         |
| 130-133 | 4    | 50-45-80-BF | Unknown Key         |
| 134-137 | 4    | B3-FA-5C-1D | Unknown Key         |
| 138-13B | 4    | 6E-08-A7-96 | Unknown Key         |
| 13C-13F | 4    | E8-39-AB-B4 | Key                 |
| 140-143 | 4    | see below   | Integer             |
| ...     | 4    | ...         | ...                 |
| 1EC-1EF | 4    | 8E-9E-12-72 | Key                 |
| 1F0-1F3 | 4    | see below   | Integer             |

## Notes

### Unknown Version

I tried changing the `2` value to `1` and `3`, this makes the game silently fail to load the character.

### CHF Version

When changing the number in the file to 0x08, this is shown in the log:

`[Error] <Unrecognised Custom Head File Version: %u> assert(false): Unrecognised Custom Head File Version: 8 [Team_S42Features][Assert]`

### Gender specific A

four 4-byte values. Always corresponds with gender. Changing this will change the head, body, and voice to the corresponding gender.

| Gender | Values                                                  |
| ------ | ------------------------------------------------------- |
| Man    | `61-4A-6B-14 / D5-39-F4-25 / 49-8A-B6-DF / 86-A4-99-A9` |
| Woman  | `AD-4C-B0-EF / 94-4A-79-D0 / 53-C2-D3-B4 / 58-25-38-AD` |
  
### Gender specific B

two 4-byte values. Always corresponds with gender.

| Gender | Values                      |
| ------ | --------------------------- |
| Man    | `F6-67-6C-DD / D3-40-E7-65` |
| Woman  | `54-EB-F4-9E / 04-52-D7-65` |

I changed these values on a default male character to the female ones. The character loads correctly and it looks pretty similar to the default male,
but the color of the head is different and shinier somehow.

### 0x40 DNA Data

I'm unsure if this is a value with key `0C-00-04-00` or something else entirely. Could even be a sequence of 16-bit integers since that seems to always fit rather well. Here are some values observed in this position:

| Gender | Values                                                                      |
| ------ | --------------------------------------------------------------------------- |
| Man    | `04-00-2B-00 / 04-00-2A-00 / 04-00-29-00 / 04-00-28-00 / 04-00-0E-00 / ...` |
| Woman  | `04-00-21-00 / 04-00-1E-00 / 04-00-1D-00 / 04-00-1C-00 / 04-00-1B-00 / ...` |

^ These values are likely which head, which part of head, and which percentage blend.

### 0x140 Integer

For all the female characters i have this is `04-00-00-00`.
For men, this can be `03-00-00-00`, `04-00-00-00` or `05-00-00-00`.

For all the ones with the value 3, the character has no hair, beard, or eyebrows.

Woman with beard: 5
woman with no hair and no beard, with brows: 4
woman with no hair or beard or brows: 3
woman with no hair or beard or brows or makeup: 3

base: 3. With hair: 4. With beard: 5. No other values observed.

Next: I am guessing these integers are the number of following data blocks. We know semi-confidently that one block will be the beard, probably another one will be the eyebrows.
Hair for some reason doesn't seem to have an influence in this value, so maybe it's not included here or it has a separate dedicated block after.

### 0x1F0 Integer

Seems to be related to children of some value. Disabling eye makeup reduces this value from 3 to 2.
Enablind eye, lip, and face makeup increases this value to 5.

Base: 2.
Each additional makeup: +1.
Max observed: 5.
