using System.Text.Json.Serialization;

namespace Mangomic.Application.DTO.Response {
    public class UserLoginResponseDTO {
        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? ExpirationDate { get; set; }

        public List<string> Errors { get; set; }

        public UserLoginResponseDTO() => Errors = new List<string>();

        public UserLoginResponseDTO(bool success = true) => Success = success;

        public UserLoginResponseDTO(bool success, string token, DateTime? expirationDate) : this(success) {
            Token = token;
            ExpirationDate = expirationDate;
        }

        public void AddError(string error) => Errors.Add(error);
    }
}
