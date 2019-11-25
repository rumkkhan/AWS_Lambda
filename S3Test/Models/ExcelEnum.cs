using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.Models
{
    public enum ExcelEnum
    {

        All = 1,
  
        General,
                 //5.  B2B (Registered person
        B2B,
        //5A. B2B Amd (Registered person)"
        B2BAMD,
        //6. B2C Large (Consumer)
        B2CLarge,
        //6A.  B2C Large Amd (Consumer )
        B2CLargeAmd,
        //7. B2C Small (Consumer )
        B2C,
        //7A.  B2C Small Amd(Consumer )
        B2CAmd,
        //8. Credit/Debit Notes
        CreditAndDebit,
        //8A.  Credit/Debit Notes Amd
        CreditAndDebitAMD,
        //9. Nil Rated/Exempt/Non GST
        NilRated,
        //10. Exports
        Exported,
        //10A. Exports Amd
        ExportedAmd,
        //11. Advance Received
        AdavanceTax,
        //11A. Amendment (Advance Received)
        AdavanceTaxAMD,
        //12. Advance Adjusted
        Taxpaid,
        //13.1. To e-commerce (Inter-State)
        eCommPartI,
        //13.2. To e-commerce (Intra-State)
        eCommPartII,
        //13.2A. To e-commerce (Intra-State) Amd
        eCommPartIIAmd,
        //14. Invoices Count
        InvoicesCount,
        //15 . Summary
        Summary,
        //16. Input Service Distributor
        ISD,
        //17. Tax Deducted at Source
        //TDS,
        //[HumanReadable("Tax Collected at Source")]
        //TCS,
        //[HumanReadable("Imports - Goods")]
        //ImportGoods,
        //[HumanReadable("Imports - Goods (AMD)")]
        //ImportGoodsAmd,
        //[HumanReadable("Imports - Service (AMD)")]
        //ImportServiceAmd,
        //[HumanReadable("Imports - Service")]
        //ImportService,
        //[HumanReadable("Input Tax Credit")]
        //ITC,
        //[HumanReadable("Tax Liability")]
        //Taxliability,
        //[HumanReadable("Tax Liability (AMD)")]
        //TaxliabilityAmd,
        //[HumanReadable("Tax Paid")]
        //TaxPaidGstr2,
        //[HumanReadable("Tax Credit Reversal")]
        //ITCReversal,
        //[HumanReadable("Tax Credit Reversal (AMD)")]
        //ITCReversalAmd,
        ////GSTR4
        //[HumanReadable("Un-Reg Inward")]
        //B2BUnRegInward,
        //[HumanReadable("Un-Reg Inward (AMD)")]
        //B2BUnRegInwardAmd,
        //[HumanReadable("Outward Supplies")]
        //OutwardSupplies,
        //[HumanReadable("Outward Supplies (AMD)")]
        //OutwardSuppliesAmd,
        //[HumanReadable("Liability Payable")]
        //LiabilityPayable,
        //[HumanReadable("CreditAndDebit UR")]
        //CreditAndDebitUR,
        //[HumanReadable("CreditAndDebit UR (AMD)")]
        //CreditAndDebitURAmd,
        //[HumanReadable("Reverse Charge(Reg/Unreg/ImportService)")]
        //B2BRevUnRegImpService,
        //[HumanReadable("Reverse Charge(Reg/Unreg/ImportService)(AMD)")]
        //B2BRevUnRegImpServiceAmd,
        //[HumanReadable("AdditionOrReduction")]
        //AdditionOrReduction,
        //[HumanReadable("RefundClaimed")]
        //RefundClaimed,
        //[HumanReadable("CashLedger")]
        //CashLedger,
        //[HumanReadable("Advance Adjusted (AMD)")]
        //TaxpaidAmd,
        //[HumanReadable("Eway")]
        //Eway,
    }
}
