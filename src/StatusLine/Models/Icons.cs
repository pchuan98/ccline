namespace StatusLine.Models;

/// <summary>
/// Icon font constants for status line plugins and components
/// </summary>
public static class Icons
{

    // Git related icons
    public const string Git = "\uf1d3";          //  - Git logo
    public const string Branch = "\uf126";       //  - Git branch
    public const string Commit = "\uf417";       //  - Git commit
    public const string Merge = "\uf1e5";        //  - Git merge
    public const string Tag = "\uf02b";          //  - Git tag
    public const string Remote = "\uf381";       //  - Git remote
    public const string PullRequest = "\uf09c";  //  - Git pull request
    public const string Fork = "\uf126";         //  - Git fork
    public const string Clone = "\uf24d";        //  - Git clone
    public const string Push = "\uf30a";         //  - Git push
    public const string Pull = "\uf30b";         //  - Git pull
    public const string Rebase = "\uf1da";       //  - Git rebase
    public const string Stash = "\uf0c7";        //  - Git stash
    public const string CherryPick = "\uf1b2";   //  - Git cherry-pick
    public const string Reset = "\uf0e2";        //  - Git reset
    public const string Revert = "\uf0e2";       //  - Git revert (same as reset)
    public const string Blame = "\uf071";        //  - Git blame
    public const string Log = "\uf1da";          //  - Git log
    public const string Diff = "\uf0ac";         //  - Git diff
    public const string Conflict = "\uf071";     //  - Git conflict
    public const string Checkout = "\uf14a";     //  - Git checkout
    public const string Status = "\uf111";       //  - Git status

    // Git workflow icons
    public const string FeatureBranch = "\uf0e8";    //  - Feature branch
    public const string ReleaseBranch = "\uf02f";    //  - Release branch
    public const string HotfixBranch = "\uf0ad";     //  - Hotfix branch
    public const string DevelopBranch = "\uf135";    //  - Develop branch

    // Git hosting services
    public const string GitHub = "\uf09b";           //  - GitHub
    public const string GitLab = "\uf296";           //  - GitLab
    public const string Bitbucket = "\uf171";        //  - Bitbucket

    // Git actions
    public const string Amend = "\uf303";            //  - Git amend
    public const string Squash = "\uf1fe";           //  - Git squash
    public const string Reflog = "\uf1da";           //  - Git reflog
    public const string Bisect = "\uf24e";           //  - Git bisect

    // Git status indicators
    public const string Staged = "\uf058";          //  - 已暂存 (绿色对勾)
    public const string Unstaged = "\uf111";        //  - 未暂存 (空心圆)
    public const string Modified = "\uf044";        //  - 已修改 (编辑图标)
    public const string Added = "\uf067";           //  - 新增文件 (加号)
    public const string Deleted = "\uf1f8";         //  - 已删除 (垃圾桶)
    public const string Renamed = "\uf0ec";         //  - 重命名 (交换箭头)
    public const string Copied = "\uf0c5";          //  - 已复制 (复制图标)
    public const string Untracked = "\uf128";       //  - 未跟踪 (问号)
    public const string Ignored = "\uf05e";         //  - 已忽略 (禁止符号)

    // 文件状态详细指示器
    public const string StagedModified = "\uf044";  //  - 已暂存的修改
    public const string StagedAdded = "\uf067";     //  - 已暂存的新增
    public const string StagedDeleted = "\uf1f8";   //  - 已暂存的删除
    public const string UnstagedModified = "\uf040";//  - 未暂存的修改 (画笔)
    public const string UnstagedAdded = "\uf196";   //  - 未暂存的新增
    public const string UnstagedDeleted = "\uf014"; //  - 未暂存的删除

    // 状态计数指示器
    public const string ChangesCount = "\uf0ae";    //  - 变更计数
    public const string StagedCount = "\uf00c";     //  - 已暂存计数
    public const string UnstagedCount = "\uf111";   //  - 未暂存计数
    public const string ConflictCount = "\uf071";   //  - 冲突计数
        
    // 状态摘要图标
    public const string CleanWorkingTree = "\uf164";//  - 工作区干净 (赞)
    public const string DirtyWorkingTree = "\uf0ad";//  - 工作区有变更 (警告)
    public const string Ahead = "\uf148";           //  - 领先远程 (向上箭头)
    public const string Behind = "\uf149";          //  - 落后远程 (向下箭头)
    public const string Diverged = "\uf362";        //  - 分叉 (双向箭头)
    public const string UpToDate = "\uf164";        //  - 与远程同步 (赞)

    // 状态组合指示器
    public const string PartiallyStaged = "\uf0c8"; //  - 部分暂存 (半填充方块)
    public const string AllStaged = "\uf14a";       //  - 全部暂存 (复选框)
    public const string NoneStaged = "\uf096";      //  - 无暂存 (空复选框)


    // File and folder icons
    public const string File = "\uf15b";         // 
    public const string Folder = "\uf07b";       // 
    public const string FolderOpen = "\uf07c";   // 
    public const string Code = "\uf121";         // 
    public const string Terminal = "\uf120";     // 

    // Status icons
    public const string Success = "\uf00c";      // 
    public const string Error = "\uf00d";        // 
    public const string Warning = "\uf071";      // 
    public const string Info = "\uf129";         // 
    public const string Loading = "\uf110";      // 
    public const string Spinner = "\uf110";      //  (备用)

    // Programming language icons
    public const string CSharp = "󰌛";       // 󰌛
    public const string JavaScript = "\uf3b8";   // 
    public const string TypeScript = "\ue628";   // 
    public const string Python = "\uf3e2";       // 
    public const string Java = "\uf4e4";         // 
    public const string Rust = "\ue7a8";         // 
    public const string Go = "\uf41b";           // 
    public const string Html = "\uf13b";         // 
    public const string Css = "\uf38b";          // 

    // Tool icons
    public const string Docker = "\uf395";       // 
    public const string Database = "\uf1c0";     // 
    public const string Server = "\uf233";       // 
    public const string Cloud = "\uf0c2";        // 
    public const string Package = "\uf466";      // 

    // General icons
    public const string Clock = "\uf017";        // 
    public const string Star = "\uf005";         // 
    public const string Heart = "\uf004";        // 
    public const string Lock = "\uf023";         // 
    public const string Key = "\uf084";          // 
    public const string Settings = "\uf013";     // 
    public const string Search = "\uf002";       // 
    public const string Filter = "\uf0b0";       // 
    public const string Sort = "\uf0dc";         // 

    // Progress and status
    public const string Play = "\uf04b";         // 
    public const string Pause = "\uf04c";        // 
    public const string Stop = "\uf04d";         // 
    public const string Refresh = "\uf021";      // 
    public const string Download = "\uf019";     // 
    public const string Upload = "\uf093";       // 

    // Navigation
    public const string ArrowLeft = "\uf060";    // 
    public const string ArrowRight = "\uf061";   // 
    public const string ArrowUp = "\uf062";      // 
    public const string ArrowDown = "\uf063";    // 
    public const string Home = "\uf015";         // 
    public const string Back = "\uf137";         // 
}
