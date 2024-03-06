export enum TransactionType { CreditByCard = 1, CreditByWireTransfer = 2, Payment = 3, CashOut = 4, CreditByUpdatePayment = 5, DebitByUpdatePayment = 6, CancelPayment = 7, 
    CancelTopupByCard = 8, DebitByTransfer = 10, CreditByTransfer = 11, Fee = 12, DebitByWireTransfer = 13, CreditByAdmin = 14, CancelTopupByAdmin = 15, CreditForCardValidation = 16, 
    DebitForCardValidation = 17, CancelDebitByWireTransfer = 18, 
    CancelPaymentWithAmount = 19, CancelCashOutWithAmount = 20, BalanceIncreaseWithoutLimit = 21, BalanceDecreaseWithoutLimit = 22, CancelDebitByWireTransferWithAmount = 23, 
    Cashback = 24, CancelCreditByWireTransfer = 25, CreditByDcb = 26, CancelTopupByDcb = 27, CancelTopupDcbWithAmount = 28, CreditByDealer = 29,CancelTopupByDealer=30,
    CancelCashback=31, CancelP2P=32, ATMWithdrawal = 33, PaymentCancellationWithPokusCard = 34, CancelPaymentCancellationWithPokusCard = 35, RebateWithPokusCard = 36, 
    CancelRebateWithPokusCard = 37, PaymentWithPokusCard = 38, CancelPaymentWithPokusCard= 39, RefundWithPokusCard = 40, CancelRefundWithPokusCard = 41, CancelDebitByTransfer = 42, 
    CancelCreditByTransfer = 43, CreditByPokusCard=44, CancelRefundTopupByDcb = 67, DebitByParentTransfer = 45, CreditByParentTransfer = 46, CancelDebitByParentTransfer = 47, 
    CancelCreditByParentTransfer = 48, CreditByKKPT = 50, CancelTopUpByKKPT=51, CreditByATM = 58, CancelTopUpByATM = 59, CancelATMWithdrawal = 49
}