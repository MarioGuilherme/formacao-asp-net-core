﻿using DevFreela.Payments.API.Models;

namespace DevFreela.Payments.API.Services;

public class PaymentService : IPaymentService {
    public Task<bool> Process(PaymentInfoInputModel paymentInfoInputModel) => Task.FromResult(true);
}