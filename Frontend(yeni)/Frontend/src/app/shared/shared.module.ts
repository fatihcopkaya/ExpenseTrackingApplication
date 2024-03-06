
import { CommonModule, DatePipe } from '@angular/common';
import { TableModule } from 'primeng/table';
import { FileUploadModule } from 'primeng/fileupload';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { DialogModule } from 'primeng/dialog';
import { PanelModule } from 'primeng/panel';
import { MessageModule } from 'primeng/message';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
import { FieldsetModule } from 'primeng/fieldset';
import { TooltipModule } from 'primeng/tooltip';
import { CheckboxModule } from 'primeng/checkbox';
import { PasswordModule } from 'primeng/password';
import { ChartModule } from 'primeng/chart';
import { MenuModule } from 'primeng/menu';
import { StyleClassModule } from 'primeng/styleclass';
import { PanelMenuModule } from 'primeng/panelmenu';
import { NgModule } from '@angular/core';
import {SelectButtonModule} from 'primeng/selectbutton';
import { InputSwitchModule } from 'primeng/inputswitch';
import { MatIconModule } from '@angular/material/icon';
import { MessagesModule } from 'primeng/messages';
import {CardModule} from 'primeng/card';
import {DividerModule} from 'primeng/divider';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {TabViewModule} from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import {BreadcrumbModule} from 'primeng/breadcrumb';
import { TagInputModule } from 'ngx-chips';
import { CaptchaModule } from 'primeng/captcha';
import { ToUpperCaseDirective } from './directives/to-upper-case.directive';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { SplitButtonModule } from 'primeng/splitbutton';
import { SplitterModule } from 'primeng/splitter';
import { SidebarModule } from 'primeng/sidebar';
import { ListboxModule } from 'primeng/listbox';
import { ActiveTypePipe } from './pipes/ActiveType.pipe';
import { DeletedTypePipe } from './pipes/DeletedType.pipe';
import { MemberTypePipe } from './pipes/MemberType.pipe';
import { PassiveReasonPipe } from './pipes/PassiveReasonPipe.pipe';
import { AccountTypePipe } from './pipes/AccountType.pipe';
import { TransactionTypePipe } from './pipes/TransationType.pipe';
import { ProcessedPipe } from './pipes/ProcessedPipe.pipe';
import { TransactionSuccesfullPipe } from './pipes/TransactionSuccesfullPipe.pipe';
import { RefundPipe } from './pipes/RefundPipe.pipe';
import { LoginSuccesfullPipe } from './pipes/LoginSuccesfullPipe.pipe';
import { isSentPipe } from './pipes/isSentPipe.pipe';
import { IsAdminTypePipe } from './pipes/IsAdmin.pipe';
import { UnMaskTypePipe } from './pipes/UnMaskType.pipe';
import { ApprovalTypePipe } from './pipes/ApprovalType.pipe';
import { ApproveStatusPipe } from './pipes/ApproveStatus.pipe';
import { HasPermissionDirective } from './directives/has-permission.directive';
import { BlackListReasonPipe } from './pipes/BlackListReason.pipe';
@NgModule({
    declarations: [
        IsAdminTypePipe,
        DeletedTypePipe,
        ActiveTypePipe,
        ToUpperCaseDirective,
        MemberTypePipe,
        PassiveReasonPipe,
        AccountTypePipe,
        ProcessedPipe,
        TransactionSuccesfullPipe,
        RefundPipe,
        AccountTypePipe,
        TransactionTypePipe,
        LoginSuccesfullPipe,
        isSentPipe,
        UnMaskTypePipe,
        ApprovalTypePipe,
        ApproveStatusPipe,
        HasPermissionDirective,
        BlackListReasonPipe
    ],
    imports: [
        
    ],
    exports: [
        CommonModule,
        TableModule,
        FileUploadModule,
        FormsModule,
        ReactiveFormsModule,
        ButtonModule,
        RippleModule,
        ToastModule,
        ToolbarModule,
        RatingModule,
        InputTextModule,
        InputTextareaModule,
        DropdownModule,
        RadioButtonModule,
        InputNumberModule,
        DialogModule,
        PanelModule,
        MessageModule,
        CalendarModule,
        MultiSelectModule,
        FieldsetModule,
        TooltipModule,
        CheckboxModule,
        PasswordModule,
        FormsModule,
        ChartModule,
        MenuModule,
        StyleClassModule,
        PanelMenuModule,       
        SelectButtonModule,
        InputSwitchModule,
        MatIconModule,
        MessagesModule,  
        CardModule,
        DividerModule,
        AutoCompleteModule,
        TabViewModule,
        TagModule,
        BreadcrumbModule,
        TagInputModule,
        CaptchaModule,
        ActiveTypePipe,
        DeletedTypePipe,
        ToUpperCaseDirective,
        ConfirmDialogModule,
        SplitterModule,
        SplitButtonModule,
        SidebarModule,
        ListboxModule,
        MemberTypePipe,
        PassiveReasonPipe,
        AccountTypePipe,
        TransactionTypePipe,
        AccountTypePipe,
        ProcessedPipe,
        TransactionSuccesfullPipe,
        RefundPipe,
        LoginSuccesfullPipe,
        isSentPipe,
        IsAdminTypePipe,
        UnMaskTypePipe,
        ApprovalTypePipe,
        ApproveStatusPipe,
        HasPermissionDirective,
        BlackListReasonPipe
        ],
    providers: [DatePipe]
})
export class SharedModule { }