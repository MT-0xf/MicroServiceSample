package com.example.bff;

import generateImageService.GenerateImageServiceOuterClass;
import interfaces.IAccountService;
import interfaces.IDownloadService;
import interfaces.IUploadService;
import mainBusiness.MainBusinessOuterClass;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.MutationMapping;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.stereotype.Controller;
import java.util.List;

/**
 * GraphQLエンドポイント
 */
@Controller
public class GraphQLEndpoint {
    /**
     * アカウントサービス
     */
    @Autowired
    IAccountService accountService;

    /**
     * ダウンロードサービス
     */
    @Autowired
    IDownloadService downloadService;

    /**
     * アップロードサービス
     */
    @Autowired
    IUploadService uploadService;

    /**
     * ログイン
     * @param name ログインユーザ名
     * @param password　パスワード
     * @return ログイン可否
     */
    @QueryMapping
    public MainBusinessOuterClass.LoginReply login(@Argument final String name, @Argument final String password) {
        MainBusinessOuterClass.LoginReply results = null;

        try {
            results = this.accountService.login(name, password);
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return results;
    }

    /**
     * 画像アップロード
     * @param title 画像タイトル
     * @param imageFile 画像ファイル
     * @return 登録可否
     */
    @MutationMapping
    public MainBusinessOuterClass.PostImageReply postImage(@Argument final String title, @Argument final String imageFile) {
        MainBusinessOuterClass.PostImageReply results = null;

        try {
            results = this.uploadService.postImage(title, imageFile);
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return results;
    }

    /**
     * 画像一覧取得
     * @return 画像一覧
     */
    @QueryMapping
    public List<MainBusinessOuterClass.PostImageFile> getPostImageList() {
        List<MainBusinessOuterClass.PostImageFile> results = null;

        try {
            results = this.downloadService.getPostImageList();
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return results;
    }

    /**
     * 生成画像ファイル取得
     * @return 生成画像
     */
    @QueryMapping
    public GenerateImageServiceOuterClass.ImageFile generateImage() {
        GenerateImageServiceOuterClass.ImageFile results = null;

        try {
            results = this.downloadService.generateImage();
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return results;
    }
}