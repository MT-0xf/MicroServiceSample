package interfaces;

import mainBusiness.MainBusinessOuterClass;
import org.springframework.graphql.data.method.annotation.Argument;

/**
 * アカウントサービスインターフェース
 */
public interface IAccountService {
    /**
     * ログイン
     * @param name ユーザ名
     * @param password パスワード
     * @return ログイン可否
     */
    MainBusinessOuterClass.LoginReply login(@Argument final String name, @Argument final String password);
}
