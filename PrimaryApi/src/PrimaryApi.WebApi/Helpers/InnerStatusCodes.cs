namespace PrimaryApi.WebApi.Helpers
{
    public enum InnerStatusCodes
    {
        /// <summary>
        /// 成功取得資源。
        /// </summary>
        GetSuccess2001 = 2001,
        /// <summary>
        /// 成功建立資源。
        /// </summary>
        CreateSuccess2002 = 2002,
        /// <summary>
        /// 成功更新資源。
        /// </summary>
        UpdateSuccess2003 = 2003,
        /// <summary>
        /// 成功移除資源。
        /// </summary>
        RemoveSuccess2004 = 2004,

        /// <summary>
        /// 找不到符合條件的資源。
        /// </summary>
        GetNotFound4001 = 4001,
        /// <summary>
        /// 找到符合條件的資源不只一個。
        /// </summary>
        GetNotSingle4002 = 4002,
        /// <summary>
        /// 欲新增的資源已存在。
        /// </summary>
        CreateExist4003 = 4003,
        /// <summary>
        /// 找不到欲更新的資源。
        /// </summary>
        UpdateNotFound4004 = 4004,
        /// <summary>
        /// 符合更新條件的資源不只一個。
        /// </summary>
        UpdateNotSingle4005 = 4005,
        /// <summary>
        /// 找不到欲移除的資源。
        /// </summary>
        RemoveNotFound4006 = 4005,
        /// <summary>
        /// 符合移除條件的資源不只一個。
        /// </summary>
        RemoveNotSingle4007 = 4007,

        /// <summary>
        /// 新增資源受到限制。
        /// </summary>
        CreateRestricted5001 = 5001,
        /// <summary>
        /// 更新資源受到限制。
        /// </summary>
        UpdateRestricted5002 = 5002,
        /// <summary>
        /// 移除資源受到限制。
        /// </summary>
        RemoveRestricted5003 = 5003
    }
}
