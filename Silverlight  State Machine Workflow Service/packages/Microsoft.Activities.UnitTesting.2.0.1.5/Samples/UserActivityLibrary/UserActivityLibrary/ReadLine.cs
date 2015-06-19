namespace UserActivityLibrary
{
    using System.Activities;

    public sealed class ReadLine : NativeActivity<string>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        protected override void Execute(NativeActivityContext context)
        {
            // Inform the host that this activity needs data and wait for the callback
            context.CreateBookmark(this.BookmarkName.Get(context), this.OnReadComplete);
        }

        private void OnReadComplete(NativeActivityContext context, Bookmark bookmark, object state)
        {
            // Store the value returned by the host
            context.SetValue(this.Result, state as string);
        }
    }
}