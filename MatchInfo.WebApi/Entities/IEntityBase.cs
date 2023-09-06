using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatchInfo.WebApi.Entities
{
    public interface IEntityBase
    {
        public int Id { get; set; }
    }
}
