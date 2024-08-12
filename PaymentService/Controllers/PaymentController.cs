using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentCodeChallenge.DTOs;
using PaymentCodeChallenge.Enums;
using PaymentCodeChallenge.Services;
// using Swashbuckle.AspNetCore.Annotations;

namespace PaymentCodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        private readonly IPaymentService _paymentService = paymentService;
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> CreatePayment([FromBody] PaymentRequest paymentRequest)
        {
            PaymentDTO paymentRes = await _paymentService.CreatePayment(PaymentDTO.Transform(paymentRequest));
            if (paymentRes == null) return NotFound("Usuario no encontrado");
            return CreatedAtAction(nameof(GetPaymentStatus), new { idPayment = paymentRes.IdPago }, paymentRes);
        }
        
        [HttpPatch("{idPayment:guid}/status")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> UpdatePaymentStatus(Guid idPayment, [FromBody] EstadoPago status)
        {
            if (!Enum.IsDefined(typeof(EstadoPago), status))
            {
                return BadRequest("El estado de pago proporcionado no es válido.");
            }
            (bool bExiste,bool bActualizado) = await _paymentService.UpdatePaymentStatus(idPayment, status);
            if (!bExiste) return NotFound();
            if (!bActualizado) return StatusCode(StatusCodes.Status500InternalServerError);
            return NoContent();
        }
        
        [HttpGet("{idPayment:guid}/status")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult> GetPaymentStatus(Guid idPayment)
        {
            PaymentDTO payment = await _paymentService.GetPayment(idPayment);
            if (payment == null) return NotFound("Pago no encontrado");
            return Ok(payment);
        }
    }
}
