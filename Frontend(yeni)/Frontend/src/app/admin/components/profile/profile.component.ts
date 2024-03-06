import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { UserInformationDTO } from 'src/app/model/userInformation';
import { ProfileInfoService } from 'src/app/services/profile-info.service';
import { id } from 'date-fns/locale';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profileInfo: UserInformationDTO = new UserInformationDTO();
  profileInfoForm: FormGroup;
  profileInfoDialog?: boolean;
  submitted?: boolean;
  isReadOnly: boolean = true; // var sayılan olarak readonly olmasını istediğimiz alanı seçtik
  updateProfileInfoDialog=false;
  loading: boolean = false;

  constructor(private route: ActivatedRoute, private _fb: FormBuilder, private profileInfoService: ProfileInfoService, private messageService: MessageService,private router:Router) { 
    this.profileInfoForm = this._fb.group({
      'firstName': [this.profileInfo.firstName || null, Validators.required],
      'lastName': [this.profileInfo.lastName ||null, Validators.required],
      'email': [this.profileInfo.email ||null, Validators.required],
      'phoneNumber': [this.profileInfo.phoneNumber ||null, Validators.required]
      // Diğer form elemanları buraya eklenebilir
    });

  }

  ngOnInit(): void {
    
    this.getProfileInfo();
    
    
  }

  loadMembers(event: any, isFilter: boolean) {}
    
  
  

   
  
  
  openNew() {
    this.profileInfo = {};
    this.submitted = false;
    this.profileInfoDialog = true;
}
  openUpdateUserInformation(id: string){
    this.profileInfo.id = id;
    this.updateProfileInfoDialog = true;
  }
  hideDialog() {
    this.profileInfoDialog = false;
    this.updateProfileInfoDialog=false;
    this.submitted = false;
  }

  getProfileInfo() {
    
    
    this.profileInfoService.getProfileInfo().subscribe((response) => {
        if ( response.message==null) {
        
        this.profileInfo = response.appUser;
        this.profileInfoForm.patchValue({
          'firstName': this.profileInfo.firstName,
          'lastName': this.profileInfo.lastName,
          'email': this.profileInfo.email,
          'phoneNumber': this.profileInfo.phoneNumber
        });
      }
    });
    
  }



  
  saveProfileInfo() {
    debugger;
    this.submitted = true;

    if (this.profileInfoForm.valid) {
      debugger;
        this.profileInfoService.updateProfileInfo(this.profileInfoForm.value).subscribe(result => {
            if (result.isSuccess) {
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı  Güncelleme Talebi İletildi.', life: 3000 });
                
            } else {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
            }
        });
    }
    this.profileInfoDialog=false
  }

  updateProfileInfo(profileInfo: UserInformationDTO) {
    debugger;
    this.profileInfo = { ...profileInfo };
    this.profileInfoDialog = true;
    this.updateProfileInfoDialog = true;
    this.isReadOnly = true; // Düzenleme modunda
  }

  submitForm() {
    debugger;
    this.submitted = true;

    if (this.profileInfoForm.valid) {
        this.saveProfileInfo();
    }
  }
}




