using System;
using ForsythBarr.Server.Controllers.Validators;
using FluentValidation.TestHelper;
using FluentValidation.Validators.UnitTestExtension;
using ForsythBarr.Server.Controllers.RequestModels;
using NUnit.Framework;
using TaskStatus=ForsythBarr.Server.Domain.Models.TaskStatus;

namespace forsythbarr.test.Validators
{
    [TestFixture]
    public class UpdateTaskRequestValidatorTests
    {
        private UpdateTaskRequestValidator _validator;

        private UpdateTaskRequest _request = new UpdateTaskRequest()
        {
            Id = default(int),
            Title = null as string,
            Description = null as string,
            DueDate = default(DateTime),
            Priority = default(int),
            Status = TaskStatus.Pending
        };

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateTaskRequestValidator();
        }
        
        [Test]
        public void Id_ShouldNotBeNull()
        {
            var result = _validator.TestValidate(_request);
            result.ShouldNotHaveValidationErrorFor( res => res.Id);
        }

        [Test]
        public void Title_ShouldNotBeNull()
        {
            var result = _validator.TestValidate(_request);
            result.ShouldHaveValidationErrorFor( res => res.Title);
        }
        
        [Test]
        public void Description_ShouldNotBeNull()
        {
            var result = _validator.TestValidate(_request);
            result.ShouldHaveValidationErrorFor( res => res.Description);
        }
        
        [Test]
        public void Status_ShouldBeValidEnumValue()
        {
            var result = _validator.TestValidate(_request);
            result.ShouldNotHaveValidationErrorFor( res => res.Status);
        
            // Invalid enum value
            _request.Status = (TaskStatus)99;
            var invalidResult = _validator.TestValidate(_request);
            invalidResult.ShouldHaveValidationErrorFor(res => res.Status);
        }
    }
}
