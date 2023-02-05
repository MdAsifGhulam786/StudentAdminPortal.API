using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetStudentsAsync();

            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("{studentId:Guid}"),ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            //Fetch student details
            var student = await studentRepository.GetStudentAsync(studentId);

            //return student
            if (student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("{studentId:Guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if (await studentRepository.Exists(studentId))
            {
                //Update details
                var updatedStudent = await studentRepository.UpdateStudentAsync(studentId, mapper.Map<DataModels.Student>(request));

                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{studentId:Guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if(await studentRepository.Exists(studentId))
            {
                var student = await studentRepository.DeleteStudentAsync(studentId);
                return Ok(mapper.Map<Student>(student));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await studentRepository.AddStudentAsync(mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },(mapper.Map<Student>(student)));
        }
    }
}
