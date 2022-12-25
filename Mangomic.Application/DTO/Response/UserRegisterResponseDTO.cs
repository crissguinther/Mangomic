namespace Mangomic.Application.DTO.Response {
    public class UserRegisterResponseDTO {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public UserRegisterResponseDTO(bool success = true) {
            Success = success;
        }

        public void AddError(IEnumerable<string> errors) {
            Errors.AddRange(errors);
        }
    }
}
