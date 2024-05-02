using ChfUtils;

namespace ChfParser;

//Below this is whatever appears after bodymaterial in the smallest files.
//the data is different, but the key-data-childcount pattern is common.
//sometimes the data is a color, integer or float, in which case the block is 12 bytes.
//some other times, the data is a guid, in which case the block is 16 + 4 + 4 = 24 bytes.
public static class TestParser
{
    public static void Read(ref SpanReader reader)
    {
        reader.ExpectBytes("E2-27-77-E8");//freckle amount
        var freckleAmount = reader.Read<float>();
        var freckleAmountChildCount = reader.Read<uint>();
        reader.ExpectBytes("58-CB-61-93");//freckle opacity
        var freckleOpacity = reader.Read<float>();
        var freckleOpacityChildCount = reader.Read<uint>();
        reader.ExpectBytes("0F-D2-4A-55");//sun spots amount
        var sunSpotsAmount = reader.Read<float>();
        var sunSpotsAmountChildCount = reader.Read<uint>();
        reader.ExpectBytes("64-12-C4-CF");//sun spot opacity
        var sunSpotOpacity = reader.Read<float>();
        var sunSpotOpacityChildCount = reader.Read<uint>();
        reader.ExpectBytes("B0-83-58-B9");//unknown
        var unknownMay0200 = reader.Read<float>();
        var unknownMay0200ChildCount = reader.Read<uint>();
        reader.ExpectBytes("C3-50-F7-9C");//unknown
        var unknownMay0201 = reader.Read<uint>();
        var unknownMay0201ChildCount = reader.Read<uint>();
        reader.ExpectBytes("DF-44-06-A9");//unknown
        var unknownMay0202 = reader.Read<uint>();
        var unknownMay0202ChildCount = reader.Read<uint>();
        reader.ExpectBytes("87-A9-71-C8");//unknown
        var unknownMay0204 = reader.Read<float>();
        var unknownMay0204ChildCount = reader.Read<uint>();
        reader.ExpectBytes("F4-7A-DE-ED");//unknown
        var unknownMay0205 = reader.Read<float>();
        var unknownMay0205ChildCount = reader.Read<uint>();
        reader.ExpectBytes("E8-6E-2F-D8");//unknown
        var unknownMay0206 = reader.Read<float>();
        var unknownMay0206ChildCount = reader.Read<uint>();
        reader.ExpectBytes("BA-26-E5-CA");//unknown
        var unknownMay0207 = reader.Read<float>();
        var unknownMay0207ChildCount = reader.Read<uint>();
        reader.ExpectBytes("02-ED-26-05");//unknown
        var unknownMay0208 = reader.Read<uint>();
        var unknownMay0208ChildCount = reader.Read<uint>();
        reader.ExpectBytes("71-3E-89-20");//unknown
        var unknownMay0209 = reader.Read<uint>();
        var unknownMay0209ChildCount = reader.Read<uint>();
        reader.ExpectBytes("6D-2A-78-15");//unknown
        var unknownMay0210 = reader.Read<uint>();
        var unknownMay0210ChildCount = reader.Read<uint>();
        reader.ExpectBytes("D7-D5-E3-9B");//unknown
        var unknownMay0211 = reader.Read<float>();
        var unknownMay0211ChildCount = reader.Read<uint>();
        reader.ExpectBytes("A4-06-4C-BE");//unknown
        var unknownMay0212 = reader.Read<float>();
        var unknownMay0212ChildCount = reader.Read<uint>();
        reader.ExpectBytes("B8-12-BD-8B");//unknown
        var unknownMay0213 = reader.Read<float>();
        var unknownMay0213ChildCount = reader.Read<uint>();
        reader.ExpectBytes("D3-A1-A1-11");//unknown
        var unknownMay0214 = reader.Read<uint>();
        var unknownMay0214ChildCount = reader.Read<uint>();
        reader.ExpectBytes("C3-1A-57-92");//unknown
        var unknownMay0215 = reader.Read<uint>();
        var unknownMay0215ChildCount = reader.Read<uint>();
        reader.ExpectBytes("B0-C9-F8-B7");//unknown
        var unknownMay0216 = reader.Read<uint>();
        var unknownMay0216ChildCount = reader.Read<uint>();
        reader.ExpectBytes("AC-DD-09-82");//unknown
        var unknownMay0217 = reader.Read<uint>();
        var unknownMay0217ChildCount = reader.Read<uint>();
        reader.ExpectBytes("E7-01-92-AA");//unknown
        var unknownMay0218 = reader.Read<float>();
        var unknownMay0218ChildCount = reader.Read<uint>();
        reader.ExpectBytes("94-D2-3D-8F");//unknown
        var unknownMay0219 = reader.Read<float>();
        var unknownMay0219ChildCount = reader.Read<uint>();
        reader.ExpectBytes("88-C6-CC-BA");//unknown
        var unknownMay0220 = reader.Read<float>();
        var unknownMay0220ChildCount = reader.Read<uint>();
        reader.ExpectBytes("F4-DC-9D-58");//unknown
        var unknownMay0221 = reader.Read<float>();
        var unknownMay0221ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x16);
        reader.Expect<uint>(0);
        reader.ExpectBytes("97-07-53-BD");//unknown
        var unknownMay0222 = reader.Read<uint>();
        var unknownMay0222ChildCount = reader.Read<uint>();
        reader.ExpectBytes("90-1D-9B-B2");//unknown
        var unknownMay0223 = reader.Read<uint>();
        var unknownMay0223ChildCount = reader.Read<uint>();
        reader.ExpectBytes("2F-0E-23-E3");//unknown
        var unknownMay0224 = reader.Read<uint>();
        var unknownMay0224ChildCount = reader.Read<uint>();
        reader.ExpectBytes("36-E7-C0-2E");//unknown
        var unknownMay0225 = reader.Read<uint>();
        var unknownMay0225ChildCount = reader.Read<uint>();
        reader.ExpectBytes("93-1A-08-1A");//unknown
        var unknownMay0226 = reader.Read<uint>();
        var unknownMay0226ChildCount = reader.Read<uint>();
        reader.ExpectBytes("2C-09-B0-4B");//unknown
        var unknownMay0227 = reader.Read<uint>();
        var unknownMay0227ChildCount = reader.Read<uint>();
        reader.ExpectBytes("35-E0-53-86");//unknown
        var unknownMay0228 = reader.Read<uint>();
        var unknownMay0228ChildCount = reader.Read<uint>();
        reader.ExpectBytes("92-E7-86-7D");//unknown
        var unknownMay0229 = reader.Read<uint>();
        var unknownMay0229ChildCount = reader.Read<uint>();
        reader.ExpectBytes("2D-F4-3E-2C");//unknown
        var unknownMay0230 = reader.Read<uint>();
        var unknownMay0230ChildCount = reader.Read<uint>();
        reader.ExpectBytes("34-1D-DD-E1");//unknown
        var unknownMay0231 = reader.Read<uint>();
        var unknownMay0231ChildCount = reader.Read<uint>();
        reader.ExpectBytes("EC-83-A5-64");//unknown
        var unknownMay0232 = reader.Read<uint>();
        var unknownMay0232ChildCount = reader.Read<uint>();
        reader.ExpectBytes("18-70-F5-77");//unknown
        var unknownMay0233 = reader.Read<uint>();
        var unknownMay0233ChildCount = reader.Read<uint>();
        reader.ExpectBytes("98-E5-F3-E9");
        var unknownMay0234 = reader.Read<uint>();
        var unknownMay0234ChildCount = reader.Read<uint>();
        reader.ExpectBytes("6C-16-A3-FA");//unknown
        var unknownMay0235 = reader.Read<uint>();
        var unknownMay0235ChildCount = reader.Read<uint>();
        reader.ExpectBytes("F2-79-B3-3C");//unknown
        var unknownMay0236 = reader.Read<uint>();
        var unknownMay0236ChildCount = reader.Read<uint>();
        reader.ExpectBytes("06-8A-E3-2F");//unknown
        var unknownMay0237 = reader.Read<uint>();
        var unknownMay0237ChildCount = reader.Read<uint>();
        reader.ExpectBytes("F1-62-B7-32");//unknown
        var unknownMay0238 = reader.Read<uint>();
        var unknownMay0238ChildCount = reader.Read<uint>();
        reader.ExpectBytes("05-91-E7-21");//unknown
        var unknownMay0239 = reader.Read<uint>();
        var unknownMay0239ChildCount = reader.Read<uint>();
        reader.ExpectBytes("57-02-E5-F7");//unknown
        var unknownMay0240 = reader.Read<uint>();
        var unknownMay0240ChildCount = reader.Read<uint>();
        reader.ExpectBytes("A3-F1-B5-E4");//unknown
        var unknownMay0241 = reader.Read<uint>();
        var unknownMay0241ChildCount = reader.Read<uint>();
        reader.ExpectBytes("D6-1F-8B-7B");//unknown
        var unknownMay0242 = reader.Read<uint>();
        var unknownMay0242ChildCount = reader.Read<uint>();
        reader.ExpectBytes("22-EC-DB-68");//unknown
        var unknownMay0243 = reader.Read<uint>();
        var unknownMay0243ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("47-69-83-6C");//unknown
        var unknownMay0244 = reader.Read<uint>();
        var unknownMay0244ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.ExpectBytes("8A-CE-74-07");//unknown
        var unknownMay0245 = reader.Read<uint>();
        var unknownMay0245ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x1);
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("17-AD-1F-1F");//unknown
        var unknownMay0246 = reader.Read<uint>();
        var unknownMay0246ChildCount = reader.Read<uint>();
        reader.Expect(0);
        reader.ExpectBytes("5A-C1-F6-4A");//unknown
        var unknownMay0247_ = reader.Read<uint>();
        var unknownMay0247_ChildCount = reader.Read<uint>();
        reader.ExpectBytes("D9-0B-37-C3");//unknown
        var unknownMay0247 = reader.Read<uint>();
        var unknownMay0247ChildCount = reader.Read<uint>();
        reader.ExpectBytes("A3-00-FA-B9");//unknown
        var unknownMay0248 = reader.Read<float>();
        var unknownMay0248ChildCount = reader.Read<uint>();
        reader.ExpectBytes("AF-F0-FB-62");//unknown
        var unknownMay0249 = reader.Read<float>();
        var unknownMay0249ChildCount = reader.Read<uint>();
        reader.ExpectBytes("76-40-08-06");//unknown
        var unknownMay0250 = reader.Read<uint>();
        var unknownMay0250ChildCount = reader.Read<uint>();
        reader.ExpectBytes("C8-A7-9A-A5");//unknown
        var unknownMay0251 = reader.Read<float>();
        var unknownMay0251ChildCount = reader.Read<uint>();
        reader.ExpectBytes("74-B6-7E-02");//unknown
        var unknownMay0252 = reader.Read<uint>();
        var unknownMay0252ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x2);
        reader.Expect<uint>(0);
        reader.ExpectBytes("14-08-E9-15");//unknown
        var unknownMay0253 = reader.Read<uint>();
        var unknownMay0253ChildCount = reader.Read<uint>();
        reader.ExpectBytes("09-C9-C7-A2");//unknown
        var unknownMay0254 = reader.Read<uint>();
        var unknownMay0254ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("93-4D-27-9B");//unknown
        var unknownMay0255 = reader.Read<uint>();
        var unknownMay0255ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.ExpectBytes("A9-5C-56-AC");//unknown
        var unknownMay0256 = reader.Read<uint>();
        var unknownMay0256ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x1);
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("9B-31-92-87");//unknown
        var unknownMay0257 = reader.Read<uint>();
        var unknownMay0257ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.ExpectBytes("5A-C1-F6-4A");//unknown
        var unknownMay0258 = reader.Read<uint>();
        var unknownMay0258ChildCount = reader.Read<uint>();
        reader.ExpectBytes("D9-0B-37-C3");//unknown
        var unknownMay0259 = reader.Read<float>();
        var unknownMay0259ChildCount = reader.Read<uint>();
        reader.ExpectBytes("A3-00-FA-B9");//unknown
        var unknownMay0260 = reader.Read<float>();
        var unknownMay0260ChildCount = reader.Read<uint>();
        reader.ExpectBytes("AF-F0-FB-62");//unknown
        var unknownMay0261 = reader.Read<float>();
        var unknownMay0261ChildCount = reader.Read<uint>();
        reader.ExpectBytes("76-40-08-06");//unknown
        var unknownMay0262 = reader.Read<uint>();
        var unknownMay0262ChildCount = reader.Read<uint>();
        reader.ExpectBytes("C8-A7-9A-A5");//unknown
        var unknownMay0263 = reader.Read<float>();
        var unknownMay0263ChildCount = reader.Read<uint>();
        reader.ExpectBytes("74-B6-7E-02");//unknown
        var unknownMay0264 = reader.Read<uint>();
        var unknownMay0264ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x2);
        reader.Expect<uint>(0);
        reader.ExpectBytes("14-08-E9-15");//unknown
        var unknownMay0265 = reader.Read<uint>();
        var unknownMay0265ChildCount = reader.Read<uint>();
        reader.ExpectBytes("09-C9-C7-A2");//unknown
        var unknownMay0266 = reader.Read<uint>();
        var unknownMay0266ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("BD-C8-8A-07");//unknown
        var unknownMay0267 = reader.Read<uint>();
        var unknownMay0267ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.ExpectBytes("6A-B7-8A-A5");//unknown
        var unknownMay0268 = reader.Read<uint>();
        var unknownMay0268ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x1);
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("10-6D-19-75");//unknown
        var unknownMay0269 = reader.Read<uint>();
        var unknownMay0269ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.ExpectBytes("5A-C1-F6-4A");//unknown
        var unknownMay0270 = reader.Read<uint>();
        var unknownMay0270ChildCount = reader.Read<uint>();
        reader.ExpectBytes("D9-0B-37-C3");//unknown
        var unknownMay0271 = reader.Read<float>();
        var unknownMay0271ChildCount = reader.Read<uint>();
        reader.ExpectBytes("A3-00-FA-B9");//unknown
        var unknownMay0272 = reader.Read<float>();
        var unknownMay0272ChildCount = reader.Read<uint>();
        reader.ExpectBytes("AF-F0-FB-62");//unknown
        var unknownMay0273 = reader.Read<float>();
        var unknownMay0273ChildCount = reader.Read<uint>();
        reader.ExpectBytes("76-40-08-06");//unknown
        var unknownMay0274 = reader.Read<uint>();
        var unknownMay0274ChildCount = reader.Read<uint>();
        reader.ExpectBytes("C8-A7-9A-A5");//unknown
        var unknownMay0275 = reader.Read<float>();
        var unknownMay0275ChildCount = reader.Read<uint>();
        reader.ExpectBytes("74-B6-7E-02");//unknown
        var unknownMay0276 = reader.Read<uint>();
        var unknownMay0276ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x2);
        reader.Expect<uint>(0);
        reader.ExpectBytes("14-08-E9-15");//unknown
        var unknownMay0277 = reader.Read<uint>();
        var unknownMay0277ChildCount = reader.Read<uint>();
        reader.ExpectBytes("09-C9-C7-A2");//unknown
        var unknownMay0278 = reader.Read<uint>();
        var unknownMay0278ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("5E-88-47-A0");//unknown
        var unknownMay0279 = reader.Read<uint>();
        var unknownMay0279ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.ExpectBytes("55-F0-9D-CE");//unknown
        var unknownMay0280 = reader.Read<uint>();
        var unknownMay0280ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x1);
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("4B-C4-36-97");//unknown
        var unknownMay0281 = reader.Read<uint>();
        var unknownMay0281ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x1);
        reader.Expect<uint>(0);
        reader.ExpectBytes("AC-34-2A-44");//unknown
        var unknownMay0282 = reader.Read<uint>();
        var unknownMay0282ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("58-4D-42-27");//unknown
        var someguid = reader.Read<Guid>();
        var additioonal = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x2);
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("A9-79-C9-73");//unknown
        var unknownMay0284 = reader.Read<uint>();
        var unknownMay0284ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x1);
        reader.Expect<uint>(0);
        reader.ExpectBytes("97-07-53-BD");//unknown
        var unknownMay0285 = reader.Read<uint>();
        var unknownMay0285ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0x5);
        reader.ExpectBytes("2C-A1-1F-A4");//unknown
        var unknownMay0286 = reader.Read<uint>();
        var unknownMay0286ChildCount = reader.Read<uint>();
        reader.Expect<uint>(0);
        reader.Expect<uint>(0x1);
        reader.Expect<uint>(0);
        reader.ExpectBytes("97-07-53-BD");//unknown
        var unknownMay0287 = reader.Read<uint>();
        var unknownMay0287ChildCount = reader.Read<uint>();
    }
}