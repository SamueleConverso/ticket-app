namespace PROGETTO_U5_S3_L5.DTOs.Account {
    public class RegisterRequestDto {
        public required string FirstName {
            get; set;
        }
        public required string LastName {
            get; set;
        }
        public required string Email {
            get; set;
        }
        public required string Password {
            get; set;
        }
    }
}
