using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace azure4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger
        private readonly IConfiguration _configuration;
        public List<AzureLearningDto> AzureModules { get; set; } = new();
        public List<AzureAssessmentDto> Assessments { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            //return;
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT ModuleName, Category, CompletionStatus, Score FROM AzureLearning", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AzureModules.Add(new AzureLearningDto
                    {
                        ModuleName = reader["ModuleName"].ToString(),
                        Category = reader["Category"].ToString(),
                        CompletionStatus = reader["CompletionStatus"].ToString(),
                        Score = reader["Score"] == DBNull.Value ? null : (int?)reader["Score"]
                    });
                }

            }

            // using (var conn = new SqlConnection(connectionString))
            // {

            //     conn.Open();
            //     var cmdAssessment = new SqlCommand("select * from AzureAssessment join AzureLearning on AzureAssessment.ModuleID = AzureLearning.ID", conn);
            //     var reader = cmdAssessment.ExecuteReader();

            //     while (reader.Read())
            //     {
            //         Assessments.Add(new AzureAssessmentDto
            //         {
            //             AssessmentId = (int)reader["AssessmentId"],
            //             ModuleId = (int)reader["ModuleId"],
            //             AttemptDate = (DateTime)reader["AttemptDate"],
            //             Score = (int)reader["Score"],
            //             Feedback = reader["Feedback"]?.ToString(),
            //             Passed = (bool)reader["Passed"],
            //             AttemptNumber = (int)reader["AttemptNumber"],
            //             ModuleName = reader["ModuleName"]?.ToString()
            //         });
            //     }

            // }
        }
    }

    public class AzureLearningDto
    {
        public string ModuleName { get; set; }
        public string Category { get; set; }
        public string CompletionStatus { get; set; }
        public int? Score { get; set; }
    }

    public class AzureAssessmentDto
    {
        public int AssessmentId { get; set; }

        public int ModuleId { get; set; }

        public DateTime AttemptDate { get; set; }

        public int Score { get; set; }

        public string? Feedback { get; set; }

        public bool Passed { get; set; }

        public int AttemptNumber { get; set; }

        // Optional: For display purposes, include module name if joining
        public string? ModuleName { get; set; }
    }
}
