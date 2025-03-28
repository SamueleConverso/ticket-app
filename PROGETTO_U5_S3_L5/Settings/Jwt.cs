namespace PROGETTO_U5_S3_L5.Settings {
    public class Jwt {
        public required string SecurityKey {
            get; set;
        }

        public required string Issuer {
            get; set;
        }

        public required string Audience {
            get; set;
        }

        public required int ExpiresInMinutes {
            get; set;
        }
    }
}
