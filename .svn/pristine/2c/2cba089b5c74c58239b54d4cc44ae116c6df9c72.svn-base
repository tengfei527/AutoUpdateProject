using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    /// <summary>
    /// 表示用于Byteart Retail领域模型中的领域服务类型。
    /// </summary>
    public class DomainService : IDomainService
    {
        #region Private Fields
        private readonly IRepositoryContext repositoryContext;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个新的<c>DomainService</c>类型的实例。
        /// </summary>
        /// <param name="repositoryContext">仓储上下文。</param>
        /// <param name="productRepository">商品仓储。</param>
        /// <param name="categoryRepository">商品分类仓储。</param>
        /// <param name="categorizationRepository">商品分类关系仓储。</param>
        /// <param name="userRepository">用户仓储。</param>
        /// <param name="roleRepository">角色仓储。</param>
        /// <param name="userRoleRepository">用户角色关系仓储。</param>
        /// <param name="shoppingCartItemRepository">购物篮项目仓储。</param>
        /// <param name="salesOrderRepository">销售订单仓储。</param>
        public DomainService(IRepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        #endregion
    }
}