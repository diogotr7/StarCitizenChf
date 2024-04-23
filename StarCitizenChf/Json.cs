namespace StarCitizenChf;

public record RootObject(
    Body body,
    string path,
    string query,
    object[] cookies
);

public record Body(
    bool hasPrevPage,
    bool hasNextPage,
    Character[] rows
);

public record Character(
    string id,
    string createdAt,
    string title,
    object[] tags,
    User user,
    string previewUrl,
    string dnaUrl,
    CharacterCounts _count
);

public record User(
    string id,
    string name,
    string image,
    string starCitizenHandle
);

public record CharacterCounts(
    int characterDownloads,
    int characterLikes
);

