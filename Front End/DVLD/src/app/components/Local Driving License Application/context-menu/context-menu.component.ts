import { NgStyle } from '@angular/common';
import { Component, EventEmitter, Output, Input } from '@angular/core';

@Component({
  selector: 'app-context-menu',
  templateUrl: './context-menu.component.html',
  styleUrls: ['./context-menu.component.css'],
  imports:[NgStyle]
})
export class ContextMenuComponent {

  @Output() optionClicked = new EventEmitter<string>();
  @Input() x = 0; // X position of the menu
  @Input() y = 0; // Y position of the menu


  onOptionClick(option: string) {
    this.optionClicked.emit(option);
  }
}