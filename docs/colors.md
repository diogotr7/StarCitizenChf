# Eye color

The game has this color picker:

![Eye color picker](img/eye_picker.png)

The color is stored in the file as 4 bytes, RGBA.
In the files I have this color seems to always be 136 bytes from the end of the binary data. Maybe it's a good idea to start reverse engineering the files backwards.

color format: 0xRR, 0xGG, 0xBB, 0xAA
