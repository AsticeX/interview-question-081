import { DatePipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzEmptyModule } from 'ng-zorro-antd/empty';

import { Comment } from '../../../core/models/comment.model';

@Component({
  selector: 'app-comment-list',
  standalone: true,
  imports: [DatePipe, NzAvatarModule, NzEmptyModule],
  templateUrl: './comment-list.component.html',
  styleUrl: './comment-list.component.scss'
})
export class CommentListComponent {
  @Input({ required: true }) comments: Comment[] = [];
}
