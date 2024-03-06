import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'TransactionTypeList'
})
export class TransactionTypePipe implements PipeTransform {

  transform(type: number): string {
    let result = "";

    switch (type) {
        case 1:
            result = 'Para Yükleme (Kart)'; //CreditByCard
            break;
        case 2:
            result = 'Para Yükleme (Havale / EFT/ Prepaid kart)'; //CreditByWireTransfer
            break;
        case 3:
            result = 'Para Çıkışı (Ödeme)'; //Payment
            break;
        case 4:
            result = 'Para Çıkışı (Nakit Çekim)'; //CashOut
            break;
        case 5:
            result = 'Para Yükleme (Ödeme Güncelleme)'; //CreditByUpdatePayment
            break;
        case 6:
            result = 'Para Çıkışı (Ödeme Güncelleme)'; //DebitByUpdatePayment
            break;
        case 7:
            result = 'Para Yükleme (Ödeme İadesi)'; //CancelPayment
            break;
        case 8:
            result = 'Para Çıkışı (Kart İle Yükleme İadesi)'; //CancelTopupByCard
            break;
        case 10:
            result = 'Para Çıkışı (Transfer)'; //DebitByTransfer
            break;
        case 11:
            result = 'Para Yükleme (Alınan Transfer)'; //CreditByTransfer
            break;
        case 12:
            result = 'İşlem Ücreti'; //Fee
            break;
        case 13:
            result = 'Para Çıkışı (Nakit Çıkışlar)'; //DebitByWireTransfer
            break;
        case 14:
            result = 'Para Yükleme (Manuel)'; //CreditByAdmin
            break;
        case 15:
            result = 'Para Çıkışı (Manuel Yükleme İptali)'; //CancelTopupByAdmin
            break;
        case 16:
            result = 'Para Yükleme (Kart Validasyonu)'; //CreditForCardValidation
            break;
        case 17:
            result = 'Para Çıkışı (Kart Validasyonu)'; //DebitForCardValidation
            break;
        case 18:
            result = 'Para Yükleme (Nakit Çıkış İptali)'; //CancelDebitByWireTransfer
            break;
        case 19:
            result = 'Para Yükleme (Parçalı Ödeme İadesi)'; //CancelPaymentWithAmount
            break;
        case 20:
            result = 'Para Yükleme (Nakit Çekim iadesi)'; //CancelCashOutWithAmount
            break;
        case 21:
            result = 'Para Yükleme (Limitsiz Bakiye Arttırımı)'; //BalanceIncreaseWithoutLimit
            break;
        case 22:
            result = 'Para Çıkışı (Limitsiz Bakiye Azaltma)'; //BalanceDecreaseWithoutLimit
            break;
        case 23:
            result = 'Para Yükleme (Parçalı Nakit Çıkış İptali)'; //CancelDebitByWireTransferWithAmount
            break;
        case 24:
            result = 'Para Yükleme (Cashback Limitsiz Bakiye Arttırımı)'; //Cashback
            break;
        case 25:
            result = 'Para Çıkışı (Havale / EFT/ Prepaid iptal)'; //CancelCreditByWireTransfer
            break;
        case 26:
            result = 'Para Yükleme (Hazır Limit)'; //CreditByDcb
            break;
        case 27:
            result = 'Para Çıkışı (Hazır Limit İptali)'; //CancelTopupByDcb
            break;
        case 28:
            result = 'Hazır Limit ile Yükleme İade'; //CancelTopupDcbWithAmount
            break;
        case 29:
            result = 'Para Yükleme (Bayi)'; //CreditByDealer
            break;
        case 30:
            result = 'Para Çıkışı (Bayiden Yükleme İadesi)'; //CancelTopupByDealer
            break;
        case 31:
            result = 'Para Çıkışı (Cashback Limitsiz Bakiye Arttırımı İptali)'; //CancelCashback
            break;
        case 32:
            result = 'CancelP2P'; //CancelP2P
            break;
        case 33:
            result = 'Para Çıkışı (ATM Para Çekme)'; //ATMWithdrawal
            break;
        case 34:
            result = 'PaymentCancellationWithPokusCard'; //PaymentCancellationWithPokusCard
            break;
        case 35:
            result = 'CancelPaymentCancellationWithPokusCard'; //CancelPaymentCancellationWithPokusCard
            break;
        case 36:
            result = 'RebateWithPokusCard'; //RebateWithPokusCard
            break;
        case 37:
            result = 'CancelRebateWithPokusCard'; //CancelRebateWithPokusCard
            break;
        case 38:
            result = 'PaymentWithPokusCard'; //PaymentWithPokusCard
            break;
        case 39:
            result = 'CancelPaymentWithPokusCard'; //CancelPaymentWithPokusCard
            break;
        case 40:
            result = 'RefundWithPokusCard'; //RefundWithPokusCard
            break;
        case 41:
            result = 'CancelRefundWithPokusCard'; //CancelRefundWithPokusCard
            break;
        case 42:
            result = 'Para Yükleme (Transfer İptal)'; //CancelDebitByTransfer
            break;
        case 43:
            result = 'Para Çıkışı (Alınan Transfer İptal)'; //CancelCreditByTransfer
            break;
        case 44:
            result = 'CreditByPokusCard'; //CreditByPokusCard
            break;
        case 45:
            result = 'Para Çıkışı (Ebeveyn-Ergen Transfer)'; //DebitByParentTransfer
            break;
        case 46:
            result = 'Para Yükleme (Ebeveyn-Ergen Alınan Transfer)'; //CreditByParentTransfer
            break;
        case 47:
            result = 'Para Çıkışı İptal (Ebeveyn-Ergen Transfer)'; //CancelDebitByParentTransfer
            break;
        case 48:
            result = 'Para Yükleme İptal (Ebeveyn-Ergen Alınan Transfer)'; //CancelCreditByParentTransfer
            break;
        case 49:
            result = 'Para Yükleme (ATM Para Çekme İptali)'; //CancelATMWithdrawal
            break;
        case 50:
            result = 'Para Yükleme (KKPT)'; //CreditByKKPT
            break;
        case 51:
            result = 'Para Çıkışı (Karttan Karta Para Transferi İptali)'; //CancelTopUpByKKPT
            break;
        case 58:
            result = 'Para Yükleme (Pokus kart)'; //CreditByATM
            break;
        case 59:
            result = 'Para Çıkışı (Pokus kart İptal)'; //CancelTopUpByATM
            break;
        case 67:
            result = 'İptal-Hazır Limit ile Yükleme İade'; //CancelRefundTopupByDcb
            break;
        default:
            result = 'Belirtilmemiş';
            break;
    }

    return result;
  }
}
