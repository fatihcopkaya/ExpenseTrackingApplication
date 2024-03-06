import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: 'input[toUpperCase]'
})
export class ToUpperCaseDirective {

  constructor() { }

  @HostListener('input', ['$event'])
  onInput(event: any) {
    event.target.value = event.target.value.toUpperCase();
  }
}