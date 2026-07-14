import { Component } from '@angular/core';

import { PostDetailComponent } from './features/posts/post-detail/post-detail.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PostDetailComponent],
  template: '<app-post-detail />'
})
export class AppComponent {}
