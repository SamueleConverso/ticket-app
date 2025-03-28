namespace PROGETTO_U5_S3_L5.DTOs.Account {
    public class TokenResponse {
        public required string Token {
            get; set;
        }
        public required DateTime Expires {
            get; set;
        }
    }
}