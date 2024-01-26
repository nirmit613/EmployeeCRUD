namespace EmployeeCrud.Services.DTO
{
    public class ResponseDTO
    {
        public int Status { get; set; }
        public object? Data { get; set; }
        public String Message { get; set; }
        public string? Error { get; set; }
    }
}
