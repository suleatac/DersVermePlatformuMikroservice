using Mikroservice.Discount.Api.Repositories;

namespace Mikroservice.Discount.Api.Features.Discounts.CreateDiscount
{
   
    public class CreateDiscountCommandHandler(AppDbContext context) : IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
      
            var discounts=context.Discounts.ToList();



            var hasCodeForUser= await context.Discounts.AnyAsync(x => x.UserId.ToString() == 
            request.UserId.ToString() && x.Code == request.Code, cancellationToken);
           
            if (hasCodeForUser)
            {
                return ServiceResult.Error("Discount code already exists for this user.",HttpStatusCode.BadRequest);
            }
            
            var discount = new Discount()
            {

                Id = NewId.NextSequentialGuid(),
                Code= request.Code,
                Rate= request.Rate,
                UserId= request.UserId,
                ExpiredTime= request.Expired,
                CreatedTime= DateTime.Now
            };
           await context.Discounts.AddAsync(discount);
           await context.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }
    }
}
